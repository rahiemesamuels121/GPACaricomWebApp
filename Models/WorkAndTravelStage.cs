namespace GPACARICOM.Models
{
    public class WorkAndTravelStage
    {
        public int StageId { get; set; }

        public string StageCode { get; set; } = string.Empty;

        public string StageName { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public int StatusId { get; set; }

        public string StatusCode { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? Notes { get; set; }
    }
}
