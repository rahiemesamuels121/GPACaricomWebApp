
namespace GPACARICOM.Models
{
    public class WorkAndTravelStageResponse
    {
        public bool Success { get; set; }
        public List<WorkAndTravelStage> Data { get; set; } = new();
    }
}
