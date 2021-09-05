using System;

namespace _288.TechTest.Data.Entities
{
    public class Discount : EntityBase<int>
    {
        public override int Id { get; set ; }
        public string Code { get; set; }
        public string CompanyId { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimumSpend { get; set; }
    }
}
