using GPACARICOM.Models;
using System.Runtime.CompilerServices;

namespace GPACARICOM.Services.API
{
    public class TestimonialApiService
    {

        private readonly ApiClient _apiClient;

        public TestimonialApiService(ApiClient apiClient)
        {
            _apiClient = apiClient;

        }
        public async Task<ApiResponse<List<TestimonialModel>>>  GetAllTestimonials() 
        {

            var response = await _apiClient.GetAsync("Testimonial/getAllTestimonials");
            return await response.Content.ReadFromJsonAsync<ApiResponse<List<TestimonialModel>>>() ?? new ApiResponse<List<TestimonialModel>>();
        }
    }
}
