using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using _288.TechTest.Domain.Interfaces;
using _288.TechTest.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IMapper mapper;

        public BasketService(IBasketRepo basketRepo, IMapper mapper)
        {
            this.basketRepo = basketRepo;
            this.mapper = mapper;
        }

        public async Task<BasketModel> GetBasketForUser(string userIdentifier, string companyIdentifier)
        {
            if (string.IsNullOrWhiteSpace(userIdentifier))
                throw new ArgumentException($"'{nameof(userIdentifier)}' cannot be null or whitespace.", nameof(userIdentifier));

            if (string.IsNullOrWhiteSpace(companyIdentifier))
                throw new ArgumentException($"'{nameof(companyIdentifier)}' cannot be null or whitespace.", nameof(companyIdentifier));

            var basket = await basketRepo.GetUsersBasket(userIdentifier, companyIdentifier);

            if (basket == null)
            {
                basket = await basketRepo.CreateUserBasket(userIdentifier, companyIdentifier);
            }

            return mapper.Map<BasketModel>(basket);
        }

        public async Task<BasketModel> UpdateBasket(UpdateBasketItemModel basketItemModel)
        {
            if (basketItemModel == null)
                throw new ArgumentNullException($"'{nameof(basketItemModel)}' cannot be null.", nameof(basketItemModel));

            if (string.IsNullOrWhiteSpace(basketItemModel.UserIdentifier))
                throw new ArgumentException($"'{nameof(basketItemModel.UserIdentifier)}' cannot be null or empty.", nameof(basketItemModel.UserIdentifier));

            if (string.IsNullOrWhiteSpace(basketItemModel.CompanyIdentifier))
                throw new ArgumentException($"'{nameof(basketItemModel.CompanyIdentifier)}' cannot be null or empty.", nameof(basketItemModel.CompanyIdentifier));

            var basket = await basketRepo.GetUsersBasket(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier);

            if (basket == null)
            {
                await basketRepo.CreateUserBasket(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier);
            }

            // Basket item already exists
            var updatedBasket = await basketRepo.UpdateBasketItem(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier, mapper.Map<BasketItem>(basketItemModel));

            return mapper.Map<BasketModel>(updatedBasket);
        }

        public async Task<BasketModel> UpdateMultipleBasketItem(UpdateMultipleBasketItemModel basketItemModel)
        {
            if (basketItemModel == null)
                throw new ArgumentNullException($"'{nameof(basketItemModel)}' cannot be null.", nameof(basketItemModel));

            if (string.IsNullOrWhiteSpace(basketItemModel.UserIdentifier))
                throw new ArgumentException($"'{nameof(basketItemModel.UserIdentifier)}' cannot be null or empty.", nameof(basketItemModel.UserIdentifier));

            if (string.IsNullOrWhiteSpace(basketItemModel.CompanyIdentifier))
                throw new ArgumentException($"'{nameof(basketItemModel.CompanyIdentifier)}' cannot be null or empty.", nameof(basketItemModel.CompanyIdentifier));

            if (basketItemModel.BasketItems == null && !basketItemModel.BasketItems.Any())
                throw new ArgumentNullException($"'{nameof(basketItemModel)}' cannot be null.", nameof(basketItemModel));

            var basket = await basketRepo.GetUsersBasket(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier);

            if (basket == null)
            {
                await basketRepo.CreateUserBasket(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier);
            }

            // Basket item already exists
            var updatedBasket = await basketRepo.UpdateMultipleBasketItem(basketItemModel.UserIdentifier, basketItemModel.CompanyIdentifier, mapper.Map<List<BasketItem>>(basketItemModel.BasketItems));

            return mapper.Map<BasketModel>(updatedBasket);
        }
    }
}
