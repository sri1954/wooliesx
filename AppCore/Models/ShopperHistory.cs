using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Models
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public List<CartItem> Products { get; set; }
    }
}
