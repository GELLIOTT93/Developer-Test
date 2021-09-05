using System.ComponentModel.DataAnnotations;

namespace _288.TechTest.Domain.Models
{
    public class GetBasketModel
    {
        [Required]
        public string UserIdentifier { get; set; }
        [Required]
        public string CompanyIdentifier { get; set; }
        public string DiscountCode { get; set; }
    }
}
