namespace GPACARICOM.Models
{
    public class Article
    {
        public int articleId { get; set; }
        public string name { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string shortDescription { get; set; } = string.Empty;
        public string fullDescription { get; set; } = string.Empty;
        public string hyperlink { get; set; } = string.Empty;

    }
}
