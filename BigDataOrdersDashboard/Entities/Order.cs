namespace BigDataOrdersDashboard.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNotes { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
