using GPACARICOM.Models;

   
    namespace GPACARICOM.Services.API
    {
        public class ProgramApiService
        {

            private readonly ApiClient _apiClient;

            public ProgramApiService(ApiClient apiClient)
            {
            _apiClient = apiClient;

            }
            public async Task<ApiResponse<List<ProgramModel>>> GetAllPrograms()
            {

            var response = await _apiClient.GetAsync("Program/getAllPrograms");

            return await response.Content.ReadFromJsonAsync<ApiResponse<List<ProgramModel>>>();
        }


        public async Task<ApiResponse<ProgramModel>> GetProgrambyId(String id)
        {
            var response = await _apiClient.GetAsync($"Program/getProgram/{id}");

            return await response.Content.ReadFromJsonAsync<ApiResponse<ProgramModel>>();

        }
 
    }
    
}
