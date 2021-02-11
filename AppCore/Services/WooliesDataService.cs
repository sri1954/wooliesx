using AppCore.Data;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class WooliesDataService : IWooliesDataService
    {
        private readonly AppDbContext _dbContext;

        public WooliesDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartItem>> ProductNameAscending()
        {
            var query = from product in _dbContext.Products
                        orderby product.Name ascending
                        select new CartItem
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        };
            return await query.ToListAsync();
        }

        public async Task<List<CartItem>> ProductNameDescending()
        {
            var query = from product in _dbContext.Products
                        orderby product.Name descending
                        select new CartItem
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        };
            return await query.ToListAsync();
        }

        public async Task<List<CartItem>> ProductPriceHighToLow()
        {
            var query = from product in _dbContext.Products
                        orderby product.Price descending
                        select new CartItem
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        };
            return await query.ToListAsync();
        }

        public async Task<List<CartItem>> ProductPriceLowToHigh()
        {
            var query = from product in _dbContext.Products
                        select new CartItem
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        };
            var list = await query.ToListAsync();
            list.Sort((a, b) => a.Price.CompareTo(b.Price));
            return list;
        }

        public async Task<List<ShopperHistory>> ProductRescommended()
        {
            List<ShopperHistory> historyItems = new List<ShopperHistory>();

            var customers = await _dbContext.Customers.ToListAsync();
            foreach (var customer in customers)
            {
                var orders = await _dbContext.Orders.Where(o => o.CustomerId == customer.CustomerId).ToListAsync();

                foreach (var customerOrder in orders)
                {
                    var query = from order in _dbContext.Orders
                                join items in _dbContext.OrderItems on order.OrderId equals items.OrderId
                                join product in _dbContext.Products on items.ProductId equals product.ProductId
                                orderby product.Name ascending
                                where order.CustomerId == customerOrder.CustomerId
                                select new CartItem
                                {
                                    Name = product.Name,
                                    Price = product.Price,
                                    Quantity = items.Quantity
                                };

                    historyItems.Add(new ShopperHistory { CustomerId = customerOrder.CustomerId, Products = await query.ToListAsync() });
                }
            }

            return historyItems;
        }

        private void notinuse()
        {
            //var list = await _dbContext.Products.ToListAsync();

            //if (string.IsNullOrEmpty(sortOption))
            //{
            //    return BadRequest();
            //}

            //sortOption = sortOption.ToLower();

            //switch (sortOption)
            //{
            //    case "low":
            //        // price low to high
            //        //list = list.OrderBy(p => p.Price).ToList();
            //        //list.Sort((a, b) => a.Price.CompareTo(b.Price));
            //        await _wooliesDataService.ProductPriceLowToHigh();
            //        break;
            //    case "high":
            //        // price high to low
            //        //list = list.OrderByDescending(p => p.Price).ToList();
            //        //list.Sort((a, b) => b.Price.CompareTo(a.Price));
            //        await _wooliesDataService.ProductPriceHighToLow();
            //        break;
            //    case "ascending":
            //        // name ascending
            //        //list = list.OrderBy(p => p.Name).ToList();
            //        //list.Sort((a, b) => a.Name.CompareTo(b.Name));
            //        await _wooliesDataService.ProductNameAscending();
            //        break;
            //    case "descending":
            //        // name descending
            //        //list = list.OrderByDescending(p => p.Name).ToList();
            //        //list.Sort((a, b) => b.Name.CompareTo(a.Name));
            //        await _wooliesDataService.ProductNameDescending();
            //        break;
            //    case "recommended":
            //        //query = query.OrderBy(p => p.Quantity);
            //        await _wooliesDataService.ProductRescommended();
            //        break;
            //    default:
            //        return BadRequest();
            //}
        }
    }
}
