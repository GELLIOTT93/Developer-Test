using System;

namespace _288.TechTest.Domain.Models
{
    public class DiscountModel
    {
        public string Code { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimumSpend { get; set; }
    }
}
