using AppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/wooliesx/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

        public UserController()
        {

        }

        // POST: api/wooliesx/User
        [HttpPost]
        public async Task<ActionResult<User>> Post(string token)
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

            await Task.Yield();
            User user = new User
            {
                Name = "Srinivasan Govintharaju",
                Token = strToken
            };

            return Ok(user);
        }
    }
}
