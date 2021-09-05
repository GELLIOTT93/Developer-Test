using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Services
{
    public class DiscountRepo : CrudRepo<Discount, DatabaseContext, int>, IDiscountRepo
    {
        private readonly DatabaseContext db;

        public DiscountRepo(DatabaseContext db) : base(db)
        {
            this.db = db;
        }

        /// <inheritdoc />
        public async Task<Discount> GetDiscountByCodeAndCustomerId(string code, string companyIdentifier)
        {
            return await db.Discounts.Where(x => 
                x.Code == code &&
                x.CompanyId == companyIdentifier &&
                x.ActiveFrom < DateTime.Now &
                x.ActiveTo > DateTime.Now
            ).FirstOrDefaultAsync();
        }
    }
}
