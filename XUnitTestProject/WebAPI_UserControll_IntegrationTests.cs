using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    public class WebAPI_UserControll_IntegrationTests : IClassFixture<WebApplicationFactory<WebAPI.Startup>>
    {
        private readonly WebApplicationFactory<WebAPI.Startup> _factory;
        private readonly HttpClient httpClient;

        public WebAPI_UserControll_IntegrationTests(WebApplicationFactory<WebAPI.Startup> factory)
        {
            // Arrange
            _factory = factory;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task HttpPost_WhenEmptyTokenPassed_ReturnsBadRequest()
        {
            // Arrange
            string strToken = string.Empty;
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/User?token=" + strToken, null);

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
        public async Task HttpPost_WhenInCorrectTokenPassed_ReturnsBadRequest()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";
            string strError = "";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/User?token=" + strToken, null);

            // Assert
            // shoud return 400 BadRequest
            // should return error invalid token

            var stringResponse = await response.Content.ReadAsStringAsync();
            JObject dynJson = (JObject)JsonConvert.DeserializeObject(stringResponse);

            foreach(var item in dynJson)
            {
                JArray jarray = (JArray)item.Value;
                strError = (string)((JValue)jarray.First).Value;
            }

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("invalid token", strError);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenPassed_ReturnsOKUserResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

            // Act
            var response = await httpClient.PostAsync("api/wooliesx/User?token=" + strToken, null);

            // Assert
            // shoud return 200 OK
            // should return User Type

            var stringResponse = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<User>(user);
            Assert.Equal("Srinivasan Govintharaju", user.Name);
            Assert.Equal("e3b5361b-e85c-4c62-85fd-4c9e2af2c053", user.Token);

        }

    }
}
