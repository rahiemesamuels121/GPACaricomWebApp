using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using GPACARICOM.Models;

namespace GPACARICOM.Services.API;

public class WATApplicationApiService(ApiClient apiClient, IConfiguration configuration)
{
    // Configure a relative route after the API endpoint has been implemented.
    private string? Endpoint => configuration["WATApplications:SubmitEndpoint"];
    public bool IsConfigured => !string.IsNullOrWhiteSpace(Endpoint);

    public async Task<ApiResponse<JsonElement>> SubmitAsync(WATApplications application)
    {
        if (!IsConfigured)
            return Failure("Online applications are not available yet. Please try again later.");

        if (!Validator.TryValidateObject(application, new ValidationContext(application), new List<ValidationResult>(), true))
            return Failure("Please correct the application details before submitting.");

        if (!application.AllAcknowledgementsApproved)
            return Failure("Please complete every acknowledgement and the applicant signature and date.");

        try
        {
            using var response = await apiClient.PostAsync(Endpoint!, application);
            if (!response.IsSuccessStatusCode)
                return Failure(response.StatusCode is System.Net.HttpStatusCode.Unauthorized or System.Net.HttpStatusCode.Forbidden
                    ? "Please sign in with an authorized account before submitting."
                    : "The application could not be submitted. Your entries are still available.");

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<JsonElement>>();
            return result?.success == true
                ? new ApiResponse<JsonElement> { success = true, message = "Your application was submitted successfully.", data = result.data }
                : Failure("The API did not confirm acceptance. Your entries are still available.");
        }
        catch (HttpRequestException)
        {
            return Failure("Submission could not be confirmed. Check with the agency before retrying to avoid a duplicate application.");
        }
        catch (OperationCanceledException)
        {
            return Failure("Submission timed out and could not be confirmed. Check with the agency before retrying.");
        }
        catch (JsonException)
        {
            return Failure("The API response could not be read, so submission is unconfirmed. Check with the agency before retrying.");
        }
    }

    private static ApiResponse<JsonElement> Failure(string message) => new() { success = false, message = message };
}
