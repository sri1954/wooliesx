using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Helper
{
    public static class FakeProduct
    {
        public static IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product{ProductId = 1, Name = "Milk", Price = 2.50, Quantity = 1},
                new Product{ProductId = 2, Name = "Bread", Price = 4.20, Quantity = 1},
                new Product{ProductId = 3, Name = "Butter", Price = 3.70, Quantity = 1},
                new Product{ProductId = 4, Name = "Lemon", Price = 20.00, Quantity = 1},
                new Product{ProductId = 5, Name = "Apple", Price = 1.90, Quantity = 1},
                new Product{ProductId = 6, Name = "Orange", Price = 3.50, Quantity = 1},
                new Product{ProductId = 7, Name = "Tomatto", Price = 7.60, Quantity = 1},
                new Product{ProductId = 8, Name = "Egg", Price = 4.75, Quantity = 1},
                new Product{ProductId = 9, Name = "Yogurt", Price = 12.75, Quantity = 1},
            };

            return products;
        }
    }
}
