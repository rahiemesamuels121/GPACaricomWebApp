namespace GPACARICOM.Models
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; } = string.Empty;

        public T? data { get; set; }


    }

    public class ApResponse<T>
    {
        public bool success { get; set; }
        public T? data { get; set; }


    }
}
