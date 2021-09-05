using _288.TechTest.Data.Interfaces;
using _288.TechTest.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace _288.TechTest.Data.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));
        }

        public static IServiceCollection RegisterDataRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBasketRepo, BasketRepo>();
            services.AddTransient<IDiscountRepo, DiscountRepo>();
            return services;
        }
}
}
