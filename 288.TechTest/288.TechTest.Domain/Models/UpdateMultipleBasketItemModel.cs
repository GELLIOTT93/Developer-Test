using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _288.TechTest.Domain.Models
{
    public class UpdateMultipleBasketItemModel
    {
        [Required]
        public string UserIdentifier { get; set; }
        [Required]
        public string CompanyIdentifier { get; set; }

        public List<UpdateBasketItemInListModel> BasketItems { get; set; }
    }
}
