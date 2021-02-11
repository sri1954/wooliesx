using AppCore.Data;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/wooliesx/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IWooliesRepository<Product> _repository;
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

        public ProductsController(IWooliesRepository<Product> repository)
        {
            _repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("token", "token is required");
                return BadRequest(ModelState);
            }

            if(token != strToken)
            {
                ModelState.AddModelError("token", "invalid token");
                return BadRequest(ModelState);

            }

            var result = await _repository.GetAll();
            return Ok(result);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _repository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
  
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError("Name", "Product name is required");
                return BadRequest(ModelState);
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Product price must be greater than zero");
                return BadRequest(ModelState);
            }

            if (product.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Product quantity must be greater than zero");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(product);

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product product)
        {
            var _product = await _repository.GetById(id);

            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError("Name", "Product name is required");
                return BadRequest(ModelState);
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError("Price", "Product price must be greater than zero");
                return BadRequest(ModelState);
            }

            if (product.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Product quantity must be greater than zero");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_product == null)
            {
                ModelState.AddModelError("Product", "Product with a productid " + id.ToString() + " is not found");
                return NotFound(ModelState);
            }

            await _repository.Update(product);

            return Ok(product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var _product = await _repository.GetById(id);
            if (_product == null)
            {
                ModelState.AddModelError("Product", "Product with a productid " + id.ToString() + " is not found");
                return NotFound(ModelState);
            }

            await _repository.DeleteById(id);

            return Ok(true);
        }
    }
}
