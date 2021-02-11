using AppCore.Data;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/wooliesx/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IWooliesDataService _wooliesDataService;
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";


        public SortController(IWooliesDataService wooliesDataService)
        {
            _wooliesDataService = wooliesDataService;
        }

        // POST: api/wooliesx/Sort
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> Post(string token, string sortOption)
        {
            List<CartItem> list = null;

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("token", "token is required");
                return BadRequest(ModelState);
            }

            if (token != strToken)
            {
                ModelState.AddModelError("token", "invalid token");
                return BadRequest(ModelState);

            }

            if (string.IsNullOrEmpty(sortOption))
            {
                ModelState.AddModelError("sortoption", "sort option required");
                return BadRequest(ModelState);
            }

            sortOption = sortOption.ToLower();

            switch (sortOption)
            {
                case "low":
                    // price low to high
                    list = await _wooliesDataService.ProductPriceLowToHigh();
                    return Ok(list);
                case "high":
                    // price high to low
                    list = await _wooliesDataService.ProductPriceHighToLow();
                    return Ok(list);
                case "ascending":
                    // name ascending
                    list = await _wooliesDataService.ProductNameAscending();
                    return Ok(list);
                case "descending":
                    // name descending
                    list = await _wooliesDataService.ProductNameDescending();
                    return Ok(list);
                case "recommended":
                    // recommended
                    var Rescommended = await _wooliesDataService.ProductRescommended();
                    return Ok(Rescommended);
                default:
                    ModelState.AddModelError("expected", "low, high, ascending, descending and recommended");
                    ModelState.AddModelError("sortoption", "invalid sort option");
                    return BadRequest(ModelState);
            }
        }
    }
}
