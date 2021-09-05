namespace _288.TechTest.Domain.Models
{
    public class BasketItemModel {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }
    }
}
