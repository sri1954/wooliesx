using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/wooliesx/[controller]")]
    [ApiController]
    public class TrollyTotalController : ControllerBase
    {
        private readonly ITrollyService _trollyService;
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

        public TrollyTotalController(ITrollyService trollyService)
        {
            _trollyService = trollyService;
        }

        // POST: api/wooliesx/ShopperHistory
        [HttpPost]
        public ActionResult<float> Post(string token, Trolly request)
        {
            float total = 0;

            try
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

                if(request == null)
                {
                    ModelState.AddModelError("request", "request is emplty");
                    return BadRequest(ModelState);
                }

                if (request.Products == null || request.Products.Count <= 0)
                {
                    ModelState.AddModelError("products", "products required");
                    return BadRequest(ModelState);
                }

                if (request.Specials == null || request.Specials.Count <= 0)
                {
                    ModelState.AddModelError("specials", "specials required");
                    return BadRequest(ModelState);
                }

                if (request.Quantities == null || request.Quantities.Count <= 0)
                {
                    ModelState.AddModelError("quantities", "quantities required");
                    return BadRequest(ModelState);
                }

                if (request.Products.Count != request.Quantities.Count)
                {
                    ModelState.AddModelError("mismatch", "same number of products and quantities required");
                    return BadRequest(ModelState);
                }

                total = _trollyService.GetTrollyTotal(request);

                return Ok(total);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
