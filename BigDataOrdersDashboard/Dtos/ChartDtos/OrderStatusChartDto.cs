namespace BigDataOrdersDashboard.Dtos.ChartDtos
{
    public class OrderStatusChartDto
    {
        public string Title { get; set; }
        public int Percentage { get; set; }
        public string ChangeText { get; set; }
        public bool IsPositive { get; set; }
        public string Color { get; set; }
    }
}
