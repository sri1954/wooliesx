using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;
using XUnitTestProject.Helper;

namespace XUnitTestProject
{
    public class WebAPI_TrollyTotalController_UnitTests
    {
        private Mock<ITrollyService> _mockService;
        private TrollyTotalController _controller;

        public WebAPI_TrollyTotalController_UnitTests()
        {
            // Arrange
            _mockService = new Mock<ITrollyService>();
        }

        [Fact]
        public void HttpPost_WhenEmptyTokenPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = string.Empty;
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error token is required
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("token is required", modelState["token"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenInCorrectTokenPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error invalid token
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("invalid token", modelState["token"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_NullRequestProductsPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            // remove request products
            request.Products = null;

            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error products required
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("products required", modelState["products"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_NullRequestSpecialsPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            // remove request specials
            request.Specials = null;

            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error specials required
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("specials required", modelState["specials"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_NullRequestQuantitiesPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            // remove request quantities
            request.Quantities = null;

            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error quantities required
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("quantities required", modelState["quantities"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_NotEqualRequestProductsAndQuantitiesPassed_ReturnsZeroTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            // add request product to make products and quantities not equal
            request.Products.Add(new TrollyProduct { Name = "Additional Product", Price = 100.00f });

            _controller = new TrollyTotalController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, request);
            var result = (BadRequestObjectResult)actionResult.Result;

            // Assert
            // should return model state error same number of products and quantities required
            // should return trolly total 0
            // shoud return 400 BadRequest

            Assert.Equal("same number of products and quantities required", modelState["mismatch"].Errors[0].ErrorMessage);
            Assert.Equal(0, actionResult.Value);
            Assert.Equal(400, result.StatusCode);

        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_EqualRequestProductsSpecialsAndQuantitiesPassed_ReturnsOKTrollyTotal()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            Trolly request = FakeTrollyRequest.GetRequest();

            _mockService.Setup(repo => repo.GetTrollyTotal(request))
                .Returns(FakeTrollyRequest.GetTrollyTotal(request));
            _controller = new TrollyTotalController(_mockService.Object);

            // Act
            _controller = new TrollyTotalController(_mockService.Object);
            var actionResult = _controller.Post(strToken, request);
            var result = (OkObjectResult)actionResult.Result;

            // Assert
            // normal price (6*6 + 2*4.5 + 1*3) = 48
            // special price 40
            // shoud return 200 OK
            // should return trolly total 40

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(40, (float)result.Value);

        }

        //[Fact]
        //public void HttpPost_WhenCorrectToken_And_EqualRequestProductsAndQuantitiesNotEqualSpecialsPassed_ReturnsOKTrollyTotal()
        //{
        //    // Arrange
        //    string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
        //    Trolly request = FakeTrollyRequest.GetRequest();

        //    _mockService.Setup(repo => repo.GetTrollyTotal(request))
        //        .Returns(FakeTrollyRequest.GetTrollyTotal(request));
        //    _controller = new TrollyTotalController(_mockService.Object);

        //    // Act
        //    _controller = new TrollyTotalController(_mockService.Object);

        //    // add request product to make products and quantities not equal
        //    request.Specials[0].Quantities.Add(new TrollyQuantity { Name = "New Product", Quantity = 1 });

        //    var actionResult = _controller.Post(strToken, request);
        //    var result = (OkObjectResult)actionResult.Result;

        //    // Assert
        //    // normal price (6*6 + 2*4.5 + 1*3) = 48
        //    // special and quantities are not same (specials has one item more than quantaties)
        //    // shoud return 200 OK
        //    // should return trolly total 48

        //    Assert.Equal(200, result.StatusCode);
        //    Assert.Equal(48, (float)result.Value);

        //}
    }
}
