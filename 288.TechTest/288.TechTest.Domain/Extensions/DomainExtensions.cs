using _288.TechTest.Domain.Interfaces;
using _288.TechTest.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _288.TechTest.Domain.Extensions
{
    public static class DomainExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IDiscountService, DiscountService>();
            return services;
        }
    }
}
