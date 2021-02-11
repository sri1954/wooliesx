using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace XUnitTestProject
{
    public class WebAPI_UserController_UnitTest
    {
        private Mock<IWooliesRepository<User>> _mockRepository;
        private UserController _controller;

        public WebAPI_UserController_UnitTest()
        {
            // Arrange
            _mockRepository = new Mock<IWooliesRepository<User>>();
        }

        [Fact]
        public async Task HttpPost_WhenEmptyTokenPassed_ReturnsBadRequest()
        {
            // Arrange
            string strToken = string.Empty;

            // Act
            _controller = new UserController();
            var modelState = _controller.ModelState;
            var badRequestResult = await _controller.Post(strToken);
            var result = (BadRequestObjectResult)badRequestResult.Result;

            // Assert
            // should return model state error token is required
            // shoud return 400 BadRequest

            Assert.Equal("token is required", modelState["token"].Errors[0].ErrorMessage);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task HttpPost_WhenInCorrectTokenPassed_ReturnsBadRequest()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";

            // Act
            _controller = new UserController();
            var modelState = _controller.ModelState;
            var badRequestResult = await _controller.Post(strToken);
            var result = (BadRequestObjectResult)badRequestResult.Result;

            // Assert
            // should return model state error invalid token
            // shoud return 400 BadRequest

            Assert.Equal("invalid token", modelState["token"].Errors[0].ErrorMessage);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task HttpPost_WhenCorrectTokenPassed_ReturnsOKUserResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

            // Act
            _controller = new UserController();
            var okObjectResult = await _controller.Post(strToken);
            var result = (OkObjectResult)okObjectResult.Result;
            var user = (User)result.Value;

            // Assert
            // shoud return 200 OK
            // should return User Type
            // name should be Srinivasan Govintharaju
            // token should be e3b5361b-e85c-4c62-85fd-4c9e2af2c053

            Assert.Equal(200, result.StatusCode);
            Assert.IsType<User>(user);

            Assert.Equal("Srinivasan Govintharaju", user.Name);
            Assert.Equal("e3b5361b-e85c-4c62-85fd-4c9e2af2c053", user.Token);
        }
    }
}
