using GPACARICOM.Models;

namespace GPACARICOM.Services.API
{
    public class WorkAndTravelApiService
    {
        private readonly ApiClient _apiClient;

        public WorkAndTravelApiService(ApiClient apiClient)
        {
            _apiClient = apiClient;

        }

        public async Task<ApiResponse<String>> SubmitApplication(WorkAndTravelApplications application)
        {
            var response = await _apiClient.PostAsync<WorkAndTravelApplications>("WorkAndTravel/applications/postApplication", application);
            return await response.Content.ReadFromJsonAsync<ApiResponse<String>>() ?? new ApiResponse<String>();
        }

        public async Task<WorkAndTravelStageResponse> GetWorkAndTravelProgressAsync()
        {
            var response = await _apiClient.GetAsync("WorkAndTravel/applications/4/progress");
            return await response.Content.ReadFromJsonAsync<WorkAndTravelStageResponse>() ?? new WorkAndTravelStageResponse() ;
        }

        public async Task<ApiResponse<string>> HasPendingWorkAndTravelApplication(int year)
        {
            var response = await _apiClient.GetAsync($"WorkAndTravel/applications/hasExistingApplication?year={year}");
            return await response.Content.ReadFromJsonAsync<ApiResponse<string>>() ?? new ApiResponse<string>();
        }

        public async Task<WorkAndTravelApplications> GetUserPendingApplication(int year)
        {
            var response = await _apiClient.GetAsync($"WorkAndTravel/applications/getExistingApplication?year={year}");
            if (!response.IsSuccessStatusCode) { 
            
            }
            return await response.Content.ReadFromJsonAsync<WorkAndTravelApplications>() ?? new WorkAndTravelApplications();
        }


    }
}






