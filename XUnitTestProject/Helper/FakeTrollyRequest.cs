using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject.Helper
{
    public static class FakeTrollyRequest
    {
        public static Trolly GetRequest()
        {

            // add products
            List<TrollyProduct> FackProducts = new List<TrollyProduct>
            {
                new TrollyProduct { Name = "Apple", Price = 6.0f },
                new TrollyProduct { Name = "Bread", Price = 4.50f },
                new TrollyProduct { Name = "Milk", Price = 3.0f },
            };

            List<TrollyQuantity> SpecialQuantities = new List<TrollyQuantity>
            {
                new TrollyQuantity { Name = "Apple", Quantity = 6},
                new TrollyQuantity { Name = "Bread", Quantity = 2},
                new TrollyQuantity { Name = "Milk", Quantity = 1},
            };
  
            // add specials
            List<Special> FackSpecials = new List<Special>
            {
                new Special{ Quantities = SpecialQuantities, Total = 40}
            };

            // add qunatities
            List<TrollyQuantity> FackQuantities = new List<TrollyQuantity>
            {
                new TrollyQuantity { Name = "Apple", Quantity = 6},
                new TrollyQuantity { Name = "Bread", Quantity = 2},
                new TrollyQuantity { Name = "Milk", Quantity = 1},
            };

            Trolly request = new Trolly
            {
                Products = FackProducts,
                Specials = FackSpecials,
                Quantities = FackQuantities
            };

            return request;
        }

        public static float GetTrollyTotal(Trolly request)
        {
            float totalSplPrice = 0;
            float totalNormalPrice = 0;
            float trollyTotal = 0;

            try
            {
                // loop through products
                foreach (var product in request.Products)
                {
                    // select a quantity
                    var trollyItem = request.Quantities.Where(q => q.Name == product.Name).FirstOrDefault();
                    if (trollyItem != null)
                    {
                        totalNormalPrice += (product.Price * trollyItem.Quantity);
                    }
                }

                totalSplPrice = GetSpecialPrice(request.Specials, request.Quantities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            trollyTotal = totalNormalPrice;

            if(totalSplPrice < totalNormalPrice)
            {
                trollyTotal = totalSplPrice;
            }

            return trollyTotal;
        }

        public static float GetSpecialPrice(List<Special> specials, List<TrollyQuantity> quantities)
        {
            float price = 0;
            bool isSpecial = true;
            try
            {

                // select a special
                foreach (var special in specials)
                {
                    foreach (var splprice in special.Quantities)
                    {
                        var trollyItem = quantities.Where(q => q.Name == splprice.Name && q.Quantity == splprice.Quantity).FirstOrDefault();

                        if (trollyItem == null)
                        {
                            isSpecial = false;
                        }
                    }

                    if (isSpecial)
                    {
                        price = special.Total;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return price;
        }
    }
}
