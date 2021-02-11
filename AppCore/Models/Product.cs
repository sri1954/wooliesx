using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppCore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [RequiredGreaterThanZero]
        [Display(Name = "Selling Price")]
        public double Price { get; set; }
        [RequiredGreaterThanZero]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }

    public class CartItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    public class RequiredGreaterThanZero : ValidationAttribute
    {
        /// <summary>
        /// Designed to validate input value is greater than zero
        /// </summary>
        /// <param name="value">Numeric</param>
        /// <returns>True if value is greater than zero</returns>
        public override bool IsValid(object value)
        {
            // return true if value is a non-null number > 0, otherwise return false
            int i;
            return value != null && int.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
