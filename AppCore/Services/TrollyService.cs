using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class TrollyService : ITrollyService
    {
        private readonly ILogger<TrollyService> _logger;

        public TrollyService(ILogger<TrollyService> logger)
        {
            _logger = logger;
        }

        public float GetTrollyTotal(Trolly request)
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

            if (totalSplPrice < totalNormalPrice)
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
