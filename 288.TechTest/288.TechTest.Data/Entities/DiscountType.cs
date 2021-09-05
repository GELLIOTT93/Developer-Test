using System.Collections.Generic;

namespace _288.TechTest.Data.Entities
{
    public class DiscountType : EntityBase<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
