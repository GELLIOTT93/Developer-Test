using System.Collections.Generic;

namespace _288.TechTest.Data.Entities
{
    public class Basket : EntityBase<int>
    {
        public string CompanyIdentifier { get; set; }
        public string UserIdentifier { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
