using System.Collections.Generic;
using System.Linq;

namespace _288.TechTest.Domain.Models
{
    public class BasketModel
    {
        public string CompanyIdentifier { get; set; }
        public string UserIdentifier { get; set; }
        public IEnumerable<BasketItemModel> BasketItems { get; set; }
        public decimal Total => BasketItems.Sum(x => x.Price * x.Quantity);

        public ProcessedBasketModel MapToProcessed(BasketModel basket)
        {
            return new ProcessedBasketModel
            {
                BasketItems = basket.BasketItems,
                CompanyIdentifier = basket.CompanyIdentifier,
                UserIdentifier = basket.UserIdentifier
            };
        }
    }

    public class ProcessedBasketModel : BasketModel {
        public decimal TotalWithDiscount { get; set; }
    }
}
