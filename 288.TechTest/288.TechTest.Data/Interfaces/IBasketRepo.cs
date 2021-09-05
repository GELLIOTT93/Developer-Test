using _288.TechTest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Interfaces
{
    public interface IBasketRepo
    {
        /// <summary>
        /// Get basket for the user based on the company id and the user id
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="companyIdentifier"></param>
        /// <returns><see cref="Basket"/></returns>
        Task<Basket> GetUsersBasket(string userIdentifier, string companyIdentifier);
        /// <summary>
        /// Create basket for user
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="companyIdentifier"></param>
        /// <returns><see cref="Basket"/></returns>
        Task<Basket> CreateUserBasket(string userIdentifier, string companyIdentifier);
        /// <summary>
        /// Add item to basket for user
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="companyIdentifier"></param>
        /// <param name="basketItem"></param>
        /// <returns><see cref="Basket"/></returns>
        Task<Basket> UpdateBasketItem(string userIdentifier, string companyIdentifier, BasketItem basketItem);
        /// <summary>
        /// Add multiple items to basket for user
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="companyIdentifier"></param>
        /// <param name="basketItems"></param>
        /// <returns><see cref="Basket"/></returns>
        Task<Basket> UpdateMultipleBasketItem(string userIdentifier, string companyIdentifier, List<BasketItem> basketItems);
    }
}
