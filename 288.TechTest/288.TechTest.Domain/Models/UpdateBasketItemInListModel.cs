﻿using _288.TechTest.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace _288.TechTest.Domain.Models
{
    public class UpdateBasketItemInListModel
    {
        [Required]
        public string ProductCode { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [NotEqualTo(0)]
        public int Quantity { get; set; }
    }
}
