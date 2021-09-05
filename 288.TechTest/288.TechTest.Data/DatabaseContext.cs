using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _288.TechTest.Data
{
    public class DatabaseContext : DbContext
    {
        private string CreatedDate => nameof(EntityBase<object>.CreatedDate);
        private string UpdatedDate => nameof(EntityBase<object>.UpdatedDate);
        private string DeletedDate => nameof(EntityBase<object>.DeletedDate);

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Discount>()
                .ExcludeSoftDeleted<Discount, int>();
            builder.Entity<DiscountType>()
                .ExcludeSoftDeleted<DiscountType, int>();
            builder.Entity<Basket>()
                .ExcludeSoftDeleted<Basket, int>();
            builder.Entity<BasketItem>()
                .ExcludeSoftDeleted<BasketItem, int>();

            builder.Entity<DiscountType>().HasData(new DiscountType[]
            {
                new DiscountType
                {
                    Id = -1,
                    Name = "Gift Voucher",
                    Description = "Removes value from whole order"
                },
                new DiscountType
                {
                    Id = -2,
                    Name = "Percent",
                    Description = "Removes percentage from whole order"
                }
            });

            builder.Entity<Discount>().HasData(new Discount[]
            {
                new Discount
                {
                    Id = -1,
                    CompanyId = "TestCompanyName",
                    Code = "5OFF",
                    DiscountTypeId = -1,
                    ActiveFrom = DateTime.Now.AddYears(-1),
                    ActiveTo = DateTime.Now.AddYears(1),
                    MinimumSpend = 10,
                    Amount = 5
                },
                new Discount
                {
                    Id = -2,
                    CompanyId = "TestCompanyName",
                    Code = "10OFF",
                    DiscountTypeId = -1,
                    ActiveFrom = DateTime.Now.AddYears(-1),
                    ActiveTo = DateTime.Now.AddYears(1),
                    MinimumSpend = 20,
                    Amount = 10
                },
                new Discount
                {
                    Id = -3,
                    CompanyId = "TestCompanyName",
                    Code = "5PERCENTOFF",
                    DiscountTypeId = -2,
                    ActiveFrom = DateTime.Now.AddYears(-1),
                    ActiveTo = DateTime.Now.AddYears(1),
                    MinimumSpend = 10,
                    Amount = 5
                },
                new Discount
                {
                    Id = -4,
                    CompanyId = "TestCompanyName",
                    Code = "10PERCENTOFF",
                    DiscountTypeId = -2,
                    ActiveFrom = DateTime.Now.AddYears(-1),
                    ActiveTo = DateTime.Now.AddYears(1),
                    MinimumSpend = 20,
                    Amount = 10
                }
            });
        }

        /// <summary>
        /// Overriding so we can set certain values when updating the data
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ChangeTracking();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracking();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracking();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ChangeTracking()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(CreatedDate)] = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(DeletedDate)] = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues[nameof(UpdatedDate)] = DateTime.Now;
                        break;
                }
            }
        }
    }
}
