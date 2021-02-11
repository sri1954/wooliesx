using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IWooliesDataService
    {
        Task<List<CartItem>> ProductPriceLowToHigh();
        Task<List<CartItem>> ProductPriceHighToLow();
        Task<List<CartItem>> ProductNameAscending();
        Task<List<CartItem>> ProductNameDescending();
        Task<List<ShopperHistory>> ProductRescommended();
    }
}
