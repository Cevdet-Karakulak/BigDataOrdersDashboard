namespace BigDataOrdersDashboard.Dtos.LoyaltyMLDtos
{
    public class ResultLoyaltyScoreMLDto
    {
        public string CustomerName { get; set; }
        public double Recency { get; set; }
        public double Frequency { get; set; }
        public double Monetary { get; set; }
        public double ActualLoyaltyScore { get; set; }
        public double PredictedLoyaltyScore { get; set; }
    }
}