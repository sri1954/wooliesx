using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    public class WebAPI_SortController_IntegrationTests : IClassFixture<WebApplicationFactory<WebAPI.Startup>>
    {
        private readonly WebApplicationFactory<WebAPI.Startup> _factory;
        private readonly HttpClient httpClient;

        public WebAPI_SortController_IntegrationTests(WebApplicationFactory<WebAPI.Startup> factory)
        {
            // Arrange
            _factory = factory;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task HttpPost_WhenEmptyTokenPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = string.Empty;
            string strOption = string.Empty;
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

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
        public async Task HttpPost_WhenInCorrectTokenPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";
            string strOption = string.Empty;
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

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
        public async Task HttpPost_WhenCorrectTokenAndEmptySortOptionPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = string.Empty;
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            // Assert
            // shoud return 400 BadRequest
            // should return error sort option required

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("sort option required", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectToken_And_InCorrectSortOptionPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "LowHigh";
            List<string> strError = new List<string>();

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            // Assert
            // shoud return 400 BadRequest
            // should return error low, high, ascending, descending and recommended
            // should return error invalid sort option

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach (var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError.Add((string)((JValue)jarray.First).Value);
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("low, high, ascending, descending and recommended", strError[0]);
            Assert.Equal("invalid sort option", strError[1]);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenAndLowSortOptionPassed_ReturnsPriceLowToHighOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Low";
            List<string> strError = new List<string>();

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            var stringResponse = await response.Content.ReadAsStringAsync();
            List<CartItem> list = JsonConvert.DeserializeObject<List<CartItem>>(stringResponse);

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Lemon", Price = 20.00, Quantity = 1} is the highest priced item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} is the lowest priced item
            // So the first item name must be Apple and Price 1.90


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(9, list.Count);
            Assert.Equal("Apple", list[0].Name);
            Assert.Equal(1.90, list[0].Price);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenAndHighSortOptionPassed_ReturnsPriceHighToLowOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "High";
            List<string> strError = new List<string>();

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            var stringResponse = await response.Content.ReadAsStringAsync();
            List<CartItem> list = JsonConvert.DeserializeObject<List<CartItem>>(stringResponse);

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Lemon", Price = 20.00, Quantity = 1} is the highest priced item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} is the lowest priced item
            // So the first item name must be Lemon and Price 20.00

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(9, list.Count);
            Assert.Equal("Lemon", list[0].Name);
            Assert.Equal(20.00, list[0].Price);

        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenAndAscendingSortOptionPassed_ReturnsNameAscendingOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Ascending";
            List<string> strError = new List<string>();

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            var stringResponse = await response.Content.ReadAsStringAsync();
            List<CartItem> list = JsonConvert.DeserializeObject<List<CartItem>>(stringResponse);

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} should be the first item
            // CartItem{Name = "Yogurt", Price = 12.75, Quantity = 1} should be the last item
            // So the first item name must be Lemon and Price 20.00

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(9, list.Count);
            Assert.Equal("Apple", list[0].Name);
            Assert.Equal(1.90, list[0].Price);

            Assert.Equal("Yogurt", list[8].Name);
            Assert.Equal(12.75, list[8].Price);

        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenAndDescendingSortOptionPassed_ReturnsNameDescendingOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Descending";
            List<string> strError = new List<string>();

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/Sort?token=" + strToken + "&sortOption=" + strOption, null);

            var stringResponse = await response.Content.ReadAsStringAsync();
            List<CartItem> list = JsonConvert.DeserializeObject<List<CartItem>>(stringResponse);

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Yogurt", Price = 12.75, Quantity = 1} should be the first item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} should be the last item
            // So the first item name must be Lemon and Price 20.00

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(9, list.Count);
            Assert.Equal("Yogurt", list[0].Name);
            Assert.Equal(12.75, list[0].Price);

            Assert.Equal("Apple", list[8].Name);
            Assert.Equal(1.90, list[8].Price);

        }

    }
}
