using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Services
{
    public class BasketRepo : IBasketRepo
    {
        private readonly DatabaseContext db;

        public BasketRepo(DatabaseContext db)
        {
            this.db = db;
        }

        /// <inheritdoc />
        public async Task<Basket> GetUsersBasket(string userIdentifier, string companyIdentifier)
        {
            return await db.Baskets.Include(x => x.BasketItems)
                .Include(x => x.BasketItems)
                .Where(x => x.UserIdentifier == userIdentifier && x.CompanyIdentifier == companyIdentifier)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<Basket> CreateUserBasket(string userIdentifier, string companyIdentifier)
        {
            var basket = db.Baskets
                .Include(x => x.BasketItems)
                .FirstOrDefault(x => x.UserIdentifier == userIdentifier && x.CompanyIdentifier == companyIdentifier);

            if (basket == null)
            {
                db.Baskets.Add(new Basket
                {
                    CompanyIdentifier = companyIdentifier,
                    UserIdentifier = userIdentifier
                });
                await db.SaveChangesAsync();
            }
            return basket;
        }

        /// <inheritdoc />
        public async Task<Basket> UpdateBasketItem(string userIdentifier, string companyIdentifier, BasketItem basketItem)
        {
            var basket = db.Baskets
               .Include(x => x.BasketItems)
               .FirstOrDefault(x => x.UserIdentifier == userIdentifier && x.CompanyIdentifier == companyIdentifier);

            if (basket == null)
                return null;

            if (basket.BasketItems != null && basket.BasketItems.Any(x => x.ProductCode == basketItem.ProductCode && x.BasketId == basket.Id))
            {
                var itemToUpdate = db.BasketItems.FirstOrDefault(x => x.ProductCode == basketItem.ProductCode && x.BasketId == basket.Id);
                itemToUpdate.Quantity += basketItem.Quantity;

                // if item is less that one it will need to be marked as deleted
                if(itemToUpdate.Quantity < 1)
                {
                    db.BasketItems.Remove(itemToUpdate);
                }
                else
                {
                    db.BasketItems.Update(itemToUpdate);
                }
            }
            else
            {
                basketItem.BasketId = basket.Id;
                db.BasketItems.Add(basketItem);
            }

            // we want to update this so that updated date.
            db.Baskets.Update(basket);

            await db.SaveChangesAsync();
            return basket;
        }

        /// <inheritdoc />
        public async Task<Basket> UpdateMultipleBasketItem(string userIdentifier, string companyIdentifier, List<BasketItem> basketItems)
        {
            var basket = db.Baskets
               .Include(x => x.BasketItems)
               .FirstOrDefault(x => x.UserIdentifier == userIdentifier && x.CompanyIdentifier == companyIdentifier);

            if (basket == null)
                return null;

            foreach (var item in basketItems)
            {
                if (basket.BasketItems != null && basket.BasketItems.Any(x => x.ProductCode == item.ProductCode && x.BasketId == basket.Id))
                {
                    var itemToUpdate = db.BasketItems.FirstOrDefault(x => x.ProductCode == item.ProductCode && x.BasketId == basket.Id);
                    itemToUpdate.Quantity += item.Quantity;

                    if (itemToUpdate.Quantity < 1)
                    {
                        db.BasketItems.Remove(itemToUpdate);
                    }
                    else
                    {
                        db.BasketItems.Update(itemToUpdate);
                    }
                }
                else
                {
                    item.BasketId = basket.Id;
                    db.BasketItems.Add(item);
                }
            }

            // we want to update this so that updated date.
            db.Baskets.Update(basket);

            await db.SaveChangesAsync();
            return basket;
        }
    }
}
