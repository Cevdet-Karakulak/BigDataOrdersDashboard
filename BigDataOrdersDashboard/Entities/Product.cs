using System.ComponentModel.DataAnnotations.Schema;

namespace BigDataOrdersDashboard.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public decimal PriceTL { get; set; }
        public Category Category { get; set; }
        public string CountryOfOrigin { get; set; }
        public string ProductImageUrl { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
