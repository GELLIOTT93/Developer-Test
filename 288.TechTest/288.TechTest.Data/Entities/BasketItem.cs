namespace _288.TechTest.Data.Entities
{
    public class BasketItem : EntityBase<int>
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// This is used to increment the quantity of them item in the basket
        /// to take quantity away use a negative number
        /// </summary>
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public Basket Basket {get;set;}
    }
}
