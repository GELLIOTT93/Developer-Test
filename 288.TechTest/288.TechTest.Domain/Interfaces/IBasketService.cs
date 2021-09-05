using _288.TechTest.Domain.Models;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Interfaces
{
    public interface IBasketService {
        Task<BasketModel> GetBasketForUser(string userIdentifier, string companyIdentifier);
        Task<BasketModel> UpdateBasket(UpdateBasketItemModel basketItemModel);
        Task<BasketModel> UpdateMultipleBasketItem(UpdateMultipleBasketItemModel basketItemModel);
    }
}
