using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTestProject.Helper;

namespace XUnitTestProject
{
    public class WebAPI_TrollyTotalController_IntegrationTests : IClassFixture<WebApplicationFactory<WebAPI.Startup>>
    {
        private readonly WebApplicationFactory<WebAPI.Startup> _factory;
        private readonly HttpClient httpClient;

        public WebAPI_TrollyTotalController_IntegrationTests(WebApplicationFactory<WebAPI.Startup> factory)
        {
            // Arrange
            _factory = factory;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task HttpPost_WhenEmptyTokenPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = string.Empty;
            Trolly request = FakeTrollyRequest.GetRequest();
            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error token is required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("token is required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenInCorrectTokenPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";
            Trolly request = FakeTrollyRequest.GetRequest();
            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error invalid token

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("invalid token", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_NullRequestProductsPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            // remove request products
            request.Products = null;

            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";


            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error products required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("products required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_NullRequestSpecialsPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            // remove request specials
            request.Specials = null;

            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";


            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error specials required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("specials required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_NullRequestQuantitiesPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            // remove request quantities
            request.Quantities = null;

            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";


            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error quantities required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("quantities required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_NotEqualRequestProductsAndQuantitiesPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            // add request product to make products and quantities not equal
            request.Products.Add(new TrollyProduct { Name = "Additional Product", Price = 100.00f });

            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strError = "";


            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // shoud return 400 BadRequest
            // should return error same number of products and quantities required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("same number of products and quantities required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_EqualRequestProductsSpecialsAndQuantitiesPassed_ReturnsOKTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            string strJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(strJson, System.Text.Encoding.UTF8, "application/json");
            string strData = "";


            // Act
            var response = await httpClient.PostAsync("api/wooliesx/TrollyTotal?token=" + strToken, stringContent);

            // Assert
            // normal price (6*6 + 2*4.5 + 1*3) = 48
            // special price 40
            // shoud return 200 OK
            // should return trolly total 40

            var stringResponse = await response.Content.ReadAsStringAsync();
     
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(40.0f, Convert.ToSingle(stringResponse));
        }

    }
}
