using GPACARICOM.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GPACARICOM.Services.API;

public class WorkAndTravelApplicationApiService(
    ApiClient apiClient,
    IConfiguration configuration)
{
    private string? Endpoint =>
        configuration["WATApplications:SubmitEndpoint"];

    private string PaymentEndpoint =>
        configuration["WATApplications:PaymentEndpoint"]
        ?? "WorkAndTravel/applications/postApplicationWithPayment";

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Endpoint);

    public async Task<ApiResponse<JsonElement>> SubmitAsync(
        WorkAndTravelApplication application)
    {
        if (!IsConfigured)
            return Failure(
                "Online applications are not available yet."
            );

        if (!Validator.TryValidateObject(
            application,
            new ValidationContext(application),
            new List<ValidationResult>(),
            true))
        {
            return Failure(
                "Please correct the application details."
            );
        }

        if (!application.AllAcknowledgementsApproved)
        {
            return Failure(
                "Please complete every acknowledgement."
            );
        }

        try
        {
            using var response = await apiClient.PostAsync(
                Endpoint!,
                application
            );

            if (!response.IsSuccessStatusCode)
            {
                return Failure(
                    response.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden
                        ? "Please sign in before submitting."
                        : "The application could not be submitted."
                );
            }

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<JsonElement>>();

            return result?.success == true
                ? new ApiResponse<JsonElement>
                {
                    success = true,
                    message = result.message
                        ?? "Application submitted successfully.",
                    data = result.data
                }
                : Failure(
                    "The API did not confirm acceptance."
                );
        }
        catch (HttpRequestException)
        {
            return Failure(
                "Submission could not be confirmed. " +
                "Check with the agency before retrying."
            );
        }
        catch (OperationCanceledException)
        {
            return Failure(
                "Submission timed out. Check before retrying."
            );
        }
        catch (JsonException)
        {
            return Failure(
                "The API response could not be read."
            );
        }
    }

    public async Task<(bool success, string message)>
        SubmitWithPaymentAsync(
            WorkAndTravelApplication application,
            WorkAndTravelPaymentRequest payment,
            IBrowserFile receipt,
            CancellationToken cancellationToken = default)
    {
        const long maxBytes = 5L * 1024 * 1024;


        // =========================================================
        // VALIDATE APPLICATION
        // =========================================================

        var applicationValidationResults =
            new List<ValidationResult>();

        if (!Validator.TryValidateObject(
            application,
            new ValidationContext(application),
            applicationValidationResults,
            true))
        {
            return (
                false,
                "Invalid application information."
            );
        }


        if (!application.AllAcknowledgementsApproved)
        {
            return (
                false,
                "Please complete all acknowledgements."
            );
        }


        // =========================================================
        // VALIDATE PAYMENT
        // =========================================================

        var paymentValidationResults =
            new List<ValidationResult>();

        if (!Validator.TryValidateObject(
            payment,
            new ValidationContext(payment),
            paymentValidationResults,
            true))
        {
            return (
                false,
                "Invalid payment information."
            );
        }


        // =========================================================
        // VALIDATE RECEIPT
        // =========================================================

        if (receipt is null || receipt.Size == 0)
        {
            return (
                false,
                "Please upload your payment receipt."
            );
        }


        if (receipt.Size > maxBytes)
        {
            return (
                false,
                "Payment receipt must not exceed 5 MB."
            );
        }


        var allowedTypes = new[]
        {
        "image/jpeg",
        "image/png",
        "image/webp"
    };


        if (!allowedTypes.Contains(
            receipt.ContentType,
            StringComparer.OrdinalIgnoreCase))
        {
            return (
                false,
                "Only JPG, PNG and WEBP images are allowed."
            );
        }


        try
        {
            // =====================================================
            // CREATE MULTIPART FORM
            // =====================================================

            using var form =
                new MultipartFormDataContent();


            // =====================================================
            // APPLICATION JSON
            // =====================================================

            var applicationJson =
                JsonSerializer.Serialize(application);


            form.Add(
                new StringContent(
                    applicationJson,
                    Encoding.UTF8,
                    "application/json"),
                "applicationJson"
            );


            // =====================================================
            // PAYMENT JSON
            // =====================================================

            var paymentJson =
                JsonSerializer.Serialize(payment);


            form.Add(
                new StringContent(
                    paymentJson,
                    Encoding.UTF8,
                    "application/json"),
                "paymentRequest"
            );


            // =====================================================
            // RECEIPT
            // =====================================================

            var receiptStream =
                receipt.OpenReadStream(
                    maxBytes,
                    cancellationToken
                );


            var fileContent =
                new StreamContent(
                    receiptStream);


            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(
                    receipt.ContentType
                );


            form.Add(
                fileContent,
                "receipt",
                receipt.Name
            );


            // =====================================================
            // SEND REQUEST
            // =====================================================

            using var response =
                await apiClient.PostMultipartAsync(
                    PaymentEndpoint,
                    form,
                    cancellationToken
                );


            // =====================================================
            // READ RESPONSE
            // =====================================================

            var responseBody =
                await response.Content.ReadAsStringAsync(
                    cancellationToken);


            // =====================================================
            // HANDLE HTTP ERROR
            // =====================================================

            if (!response.IsSuccessStatusCode)
            {
                if (string.IsNullOrWhiteSpace(
                    responseBody))
                {
                    return (
                        false,
                        $"Application submission failed. " +
                        $"Status: {(int)response.StatusCode} " +
                        $"{response.ReasonPhrase}"
                    );
                }


                try
                {
                    using var errorJson =
                        JsonDocument.Parse(
                            responseBody);


                    if (errorJson.RootElement.TryGetProperty(
                        "message",
                        out var messageElement))
                    {
                        return (
                            false,
                            messageElement.GetString()
                            ?? "Application submission failed."
                        );
                    }
                }
                catch (JsonException)
                {
                    // Response was not JSON.
                }


                return (
                    false,
                    responseBody
                );
            }


            // =====================================================
            // HANDLE EMPTY SUCCESS RESPONSE
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                responseBody))
            {
                return (
                    true,
                    "Application submitted successfully."
                );
            }


            // =====================================================
            // PARSE SUCCESS RESPONSE
            // =====================================================

            try
            {
                using var responseJson =
                    JsonDocument.Parse(
                        responseBody);


                var root =
                    responseJson.RootElement;


                bool success = true;

                string message =
                    "Application submitted successfully.";


                if (root.TryGetProperty(
                    "success",
                    out var successElement))
                {
                    success =
                        successElement.GetBoolean();
                }


                if (root.TryGetProperty(
                    "message",
                    out var messageElement))
                {
                    message =
                        messageElement.GetString()
                        ?? message;
                }


                return (
                    success,
                    message
                );
            }
            catch (JsonException)
            {
                return (
                    true,
                    "Application submitted successfully."
                );
            }
        }
        catch (OperationCanceledException)
        {
            return (
                false,
                "Application submission was cancelled."
            );
        }
        catch (HttpRequestException ex)
        {
            return (
                false,
                $"Unable to connect to the server: {ex.Message}"
            );
        }
        catch (Exception ex)
        {
            return (
                false,
                $"An unexpected error occurred: {ex.Message}"
            );
        }


    }
    public async Task<ApiResponse<WorkAndTravelApplication>>
    GetUserPendingApplication(
        int year,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var response =
                await apiClient.GetAsync(
                    $"WorkAndTravel/applications/getExistingApplication?year={year}"
                );

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<WorkAndTravelApplication>
                {
                    success = false,
                    message =
                        "Unable to check for an existing application.",
                    data = null
                };
            }

            var result =
                await response.Content
                    .ReadFromJsonAsync<ApiResponse<WorkAndTravelApplication>>(
                        cancellationToken:
                            cancellationToken
                    );

            return result ??
                new ApiResponse<WorkAndTravelApplication>
                {
                    success = false,
                    message =
                        "The API returned an empty response.",
                    data = null
                };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<WorkAndTravelApplication>
            {
                success = false,
                message =
                    $"Unable to connect to the server: {ex.Message}",
                data = null
            };
        }
        catch (OperationCanceledException)
        {
            return new ApiResponse<WorkAndTravelApplication>
            {
                success = false,
                message =
                    "The request was cancelled.",
                data = null
            };
        }
    }

    public async Task<ApiResponse<WorkAndTravelStageResponse>> GetWorkAndTravelProgressAsync(
        int year)
    {
        try
        {
            using var response =
                await apiClient.GetAsync(
                    $"WorkAndTravel/applications/progress?year={year}");

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<WorkAndTravelStageResponse>
                {
                    success = false,
                    message =
                        "Unable to check the application progress.",
                    data = null
                };
            }

            var result =
                await response.Content
                    .ReadFromJsonAsync<ApiResponse<WorkAndTravelStageResponse>>();

            return result ??
                new ApiResponse<WorkAndTravelStageResponse>
                {
                    success = false,
                    message =
                        "The API returned an empty response.",
                    data = null
                };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<WorkAndTravelStageResponse>
            {
                success = false,
                message =
                    $"Unable to connect to the server: {ex.Message}",
                data = null
            };
        }
        catch (OperationCanceledException)
        {
            return new ApiResponse<WorkAndTravelStageResponse>
            {
                success = false,
                message =
                    "The request was cancelled.",
                data = null
            };
        }
    }

    public async Task<ApiResponse<bool>> HasPendingWorkAndTravelApplication(
        int year)
    {
        try
        {
            using var response =
                await apiClient.GetAsync(
                    $"WorkAndTravel/applications/hasExistingApplication?year={year}");

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool>
                {
                    success = false,
                    message =
                        "Unable to check for an existing application.",
                    data = false
                };
            }

            return await response.Content
                       .ReadFromJsonAsync<ApiResponse<bool>>()
                   ?? new ApiResponse<bool>
                   {
                       success = false,
                       message =
                           "The API returned an empty response.",
                       data = false
                   };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<bool>
            {
                success = false,
                message =
                    $"Unable to connect to the server: {ex.Message}",
                data = false
            };
        }
        catch (OperationCanceledException)
        {
            return new ApiResponse<bool>
            {
                success = false,
                message = "The request was cancelled.",
                data = false
            };
        }
    }

    private static ApiResponse<JsonElement> Failure(
        string message) => new()
        {
            success = false,
            message = message
        };
}