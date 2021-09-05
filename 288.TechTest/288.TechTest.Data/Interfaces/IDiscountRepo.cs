using _288.TechTest.Data.Entities;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Interfaces
{
    public interface IDiscountRepo
    {
        /// <summary>
        /// Used to get discounts by their code and to the relevant company.
        /// </summary>
        /// <param name="code">The </param>
        /// <param name="companyIdentifier"></param>
        /// <returns></returns>
        Task<Discount> GetDiscountByCodeAndCustomerId(string code, string companyIdentifier);
    }
}
