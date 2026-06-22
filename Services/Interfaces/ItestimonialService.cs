using GPACARICOM.Models;

namespace GPACARICOM.Services.Interfaces
{
    public interface ItestimonialService
    {
        public Task<List<TestimonialModel>> GetTestimonialsAsync();
        Article GetArticle(int id);
        bool AddNewTestimonial(Article article);
        bool UpDateTestimonial(Article article);
        bool DeleteTestimonial(int id);
    }
}
