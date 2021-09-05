using _288.TechTest.Domain.Models;
using System.Threading.Tasks;

namespace _288.TechTest.Domain.Interfaces
{
    public interface IDiscountService
    {
        /// <summary>
        /// Used to get a discount code for a company
        /// </summary>
        /// <param name="code"></param>
        /// <param name="companyId"></param>
        /// <returns><see cref="DiscountModel"/></returns>
        Task<DiscountModel> GetDiscountByCodeAndCompanyId(string code, string companyId);
    }
}