using Microsoft.ML.Data;

namespace BigDataOrdersDashboard.Dtos.LoyaltyMLDtos
{
    public class LoyaltyScoreMLPredictionDto
    {
        [ColumnName("Score")]
        public float LoyaltyScore { get; set; }
    }
}
