using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WXFunctionApp.Models;

namespace WXFunctionApp
{
    public static class WXChallange
    {
        [FunctionName("user")]
        public static IActionResult user(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

            // create user
            user _user = new user
            {
                name = "Srinivasan Govintharaju",
                token = strToken
            };

            // json serialise
            string responseMessage = System.Text.Json.JsonSerializer.Serialize(_user);

            // return response
            return new OkObjectResult(responseMessage);
        }

        [FunctionName("sort")]
        public static IActionResult sort(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

            // create user
            user _user = new user
            {
                name = "Srinivasan Govintharaju",
                token = strToken
            };

            // json serialise
            string responseMessage = System.Text.Json.JsonSerializer.Serialize(_user);

            // return response
            return new OkObjectResult(responseMessage);
        }
    }
}
