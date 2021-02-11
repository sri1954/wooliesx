using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject.Helper
{
    public static class FakeCartItems
    {
        public static List<CartItem> GetCartItems()
        {
            List<CartItem> cartItems = new List<CartItem>
            {
                new CartItem{Name = "Yogurt", Price = 12.75, Quantity = 1},
                new CartItem{Name = "Bread", Price = 4.20, Quantity = 1},
                new CartItem{Name = "Butter", Price = 3.70, Quantity = 1},
                new CartItem{Name = "Lemon", Price = 20.00, Quantity = 1},
                new CartItem{Name = "Apple", Price = 1.90, Quantity = 1},
                new CartItem{Name = "Orange", Price = 3.50, Quantity = 1},
                new CartItem{Name = "Tomatto", Price = 7.60, Quantity = 1},
                new CartItem{Name = "Egg", Price = 4.75, Quantity = 1},
                new CartItem{Name = "Milk", Price = 2.50, Quantity = 1},
            };

            return cartItems;
        }

        public static List<CartItem> GetProductPriceLowToHigh()
        {
            var cartItems = GetCartItems();
            cartItems.Sort((a, b) => a.Price.CompareTo(b.Price));
            return cartItems;
        }

        public static List<CartItem> GetProductPriceHighToLow()
        {
            var cartItems = GetCartItems();
            cartItems.Sort((a, b) => b.Price.CompareTo(a.Price));
            return cartItems;
        }

        public static List<CartItem> GetProductNameAscending()
        {
            var cartItems = GetCartItems();
            cartItems.Sort((a, b) => a.Name.CompareTo(b.Name));
            return cartItems;
        }

        public static List<CartItem> GetProductNameDescending()
        {
            var cartItems = GetCartItems();
            cartItems.Sort((a, b) => b.Name.CompareTo(a.Name));
            return cartItems;
        }

        public static List<CartItem> GetProductRescommended()
        {
            var cartItems = GetCartItems();
            cartItems.Sort((a, b) => a.Price.CompareTo(b.Price));
            return cartItems;
        }

    }
}
