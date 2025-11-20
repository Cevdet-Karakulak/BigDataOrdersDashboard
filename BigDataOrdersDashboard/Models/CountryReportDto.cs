namespace BigDataOrdersDashboard.Models
{
    public class CountryReportDto
    {
        public string Country { get; set; }
        public int Total2023 { get; set; }
        public int Total2024 { get; set; }
        public decimal ChangeRate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
