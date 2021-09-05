using _288.TechTest.Data.Interfaces;
using _288.TechTest.Domain.Interfaces;
using _288.TechTest.Domain.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper mapper;
        private readonly IDiscountRepo discountRepo;

        public DiscountService(IMapper mapper, IDiscountRepo discountRepo)
        {
            this.mapper = mapper;
            this.discountRepo = discountRepo;
        }

        /// <inheritdoc />
        public async Task<DiscountModel> GetDiscountByCodeAndCompanyId(string code, string companyId)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"'{nameof(code)}' cannot be null or whitespace.", nameof(code));

            if (string.IsNullOrWhiteSpace(companyId))
                throw new ArgumentException($"'{nameof(companyId)}' cannot be null or whitespace.", nameof(companyId));

            var discount = await discountRepo.GetDiscountByCodeAndCustomerId(code, companyId);

            return mapper.Map<DiscountModel>(discount);
        }
    }
}
