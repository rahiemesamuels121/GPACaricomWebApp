namespace GPACARICOM.Models
{
    public class TestimonialModel
    {
        public int testimonialid { get; set; }
        public int testimonialUserid { get; set; }
        public string testimonialTitle { get; set; } = string.Empty;
        public string testimonialbody { get; set; } = string.Empty;
        public DateTime createdDate { get; set; } = DateTime.Now;

    }
}
