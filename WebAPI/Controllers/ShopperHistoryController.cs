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
    public class ShopperHistoryController : ControllerBase
    {
        private readonly IWooliesDataService _wooliesDataService;
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

        public ShopperHistoryController(IWooliesDataService wooliesDataService)
        {
            _wooliesDataService = wooliesDataService;
        }

        // POST: api/wooliesx/ShopperHistory
        [HttpPost]
        public async Task<ActionResult> Post(string token)
        {
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

            return Ok(await _wooliesDataService.ProductRescommended());
        }
    }
}
