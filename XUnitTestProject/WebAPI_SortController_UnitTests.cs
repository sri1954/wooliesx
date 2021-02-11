using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebAPI.Controllers;
using Xunit;
using XUnitTestProject.Helper;

namespace XUnitTestProject
{
    public class WebAPI_SortController_UnitTests
    {
        private Mock<IWooliesDataService> _mockService;
        private SortController _controller;

        public WebAPI_SortController_UnitTests()
        {
            // Arrange
            _mockService = new Mock<IWooliesDataService>();
        }

        [Fact]
        public void HttpPost_WhenEmptyTokenPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = string.Empty;
            string strOption = string.Empty;

            // Act
            _controller = new SortController(_mockService.Object);
            var modelState = _controller.ModelState;
            var actionResult = _controller.Post(strToken, strOption);
            var result = (List<Product>)actionResult.Result.Value;

            // Assert
            // should return model state error token is required
            // shoud return 400 BadRequest
            // List<Product> must be Null

            Assert.Equal("token is required", modelState["token"].Errors[0].ErrorMessage);
            Assert.Null(result);
        }

        [Fact]
        public void HttpPost_WhenInCorrectTokenPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053_11111";
            string strOption = string.Empty;

            // Act
            _controller = new SortController(_mockService.Object);

            var badRequestResult = _controller.Post(strToken, strOption);
            var modelState = _controller.ModelState;

            var result = (List<Product>)badRequestResult.Result.Value;

            // Assert
            // should return model state error invalid token
            // shoud return 400 BadRequest
            // List<Product> must be Null

            Assert.Equal("invalid token", modelState["token"].Errors[0].ErrorMessage);
            Assert.Null(result);
        }

        [Fact]
        public void HttpPost_WhenCorrectTokenAndEmptySortOptionPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = string.Empty;

            // Act
            _controller = new SortController(_mockService.Object);

            var badRequestResult = _controller.Post(strToken, strOption);
            var modelState = _controller.ModelState;

            var result = (List<Product>)badRequestResult.Result.Value;

            // Assert
            // should return model state error sort option required
            // shoud return 400 BadRequest
            // List<Product> must be Null

            Assert.Equal("sort option required", modelState["sortoption"].Errors[0].ErrorMessage);
            Assert.Null(result);
        }

        [Fact]
        public void HttpPost_WhenCorrectToken_And_InCorrectSortOptionPassed_ReturnsBadRequestResult()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "LowHigh";

            // Act
            _controller = new SortController(_mockService.Object);

            var badRequestResult = _controller.Post(strToken, strOption);
            var modelState = _controller.ModelState;
            var result = (List<Product>)badRequestResult.Result.Value;

            // Assert
            // should return model state error invalid sort option
            // should return model state error low, high, ascending, descending and recommended
            // shoud return 400 BadRequest
            // List<Product> must be Null

            Assert.Equal("low, high, ascending, descending and recommended", modelState["expected"].Errors[0].ErrorMessage);
            Assert.Equal("invalid sort option", modelState["sortoption"].Errors[0].ErrorMessage);
            Assert.Null(result);
        }

        [Fact]
        public void HttpPost_WhenCorrectTokenAndLowSortOptionPassed_ReturnsPriceLowToHighOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Low";

            _mockService.Setup(repo => repo.ProductPriceLowToHigh())
                .ReturnsAsync(FakeCartItems.GetProductPriceLowToHigh());
            _controller = new SortController(_mockService.Object);


            // Act
            _controller = new SortController(_mockService.Object);
            var okObjectResult = _controller.Post(strToken, strOption);
            var result = (OkObjectResult)okObjectResult.Result.Result;
            var list = (List<CartItem>)result.Value;

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Lemon", Price = 20.00, Quantity = 1} is the highest priced item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} is the lowest priced item
            // So the first item name must be Apple and Price 1.90

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(9, list.Count);
            Assert.Equal("Apple", list[0].Name);
            Assert.Equal(1.90, list[0].Price);
        }

        [Fact]
        public void HttpPost_WhenCorrectTokenAndHighSortOptionPassed_ReturnsPriceHighToLowOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "High";

            _mockService.Setup(repo => repo.ProductPriceHighToLow())
                .ReturnsAsync(FakeCartItems.GetProductPriceHighToLow);
            _controller = new SortController(_mockService.Object);


            // Act
            _controller = new SortController(_mockService.Object);
            var okObjectResult = _controller.Post(strToken, strOption);
            var result = (OkObjectResult)okObjectResult.Result.Result;
            var list = (List<CartItem>)result.Value;

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Lemon", Price = 20.00, Quantity = 1} is the highest priced item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} is the lowest priced item
            // So the first item name must be Lemon and Price 20.00

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(9, list.Count);
            Assert.Equal("Lemon", list[0].Name);
            Assert.Equal(20.00, list[0].Price);
        }

        [Fact]
        public void HttpPost_WhenCorrectTokenAndAscendingSortOptionPassed_ReturnsNameAscendingOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Ascending";

            _mockService.Setup(repo => repo.ProductNameAscending())
                .ReturnsAsync(FakeCartItems.GetProductNameAscending);
            _controller = new SortController(_mockService.Object);


            // Act
            _controller = new SortController(_mockService.Object);
            var okObjectResult = _controller.Post(strToken, strOption);
            var result = (OkObjectResult)okObjectResult.Result.Result;
            var list = (List<CartItem>)result.Value;

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} should be the first item
            // CartItem{Name = "Yogurt", Price = 12.75, Quantity = 1} should be the last item
            // So the first item name must be Lemon and Price 20.00

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(9, list.Count);

            Assert.Equal("Apple", list[0].Name);
            Assert.Equal(1.90, list[0].Price);

            Assert.Equal("Yogurt", list[8].Name);
            Assert.Equal(12.75, list[8].Price);
        }

        [Fact]
        public void HttpPost_WhenCorrectTokenAndDescendingSortOptionPassed_ReturnsNameDescendingOKSortedList()
        {
            // Arrange
            string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";
            string strOption = "Descending";

            _mockService.Setup(repo => repo.ProductNameDescending())
                .ReturnsAsync(FakeCartItems.GetProductNameDescending);
            _controller = new SortController(_mockService.Object);

            // Act
            _controller = new SortController(_mockService.Object);
            var okObjectResult = _controller.Post(strToken, strOption);
            var result = (OkObjectResult)okObjectResult.Result.Result;
            var list = (List<CartItem>)result.Value;

            // Assert
            // the fake cart items list has 9 items
            // CartItem{Name = "Yogurt", Price = 12.75, Quantity = 1} should be the first item
            // CartItem{Name = "Apple", Price = 1.90, Quantity = 1} should be the last item
            // So the first item name must be Lemon and Price 20.00

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(9, list.Count);

            Assert.Equal("Yogurt", list[0].Name);
            Assert.Equal(12.75, list[0].Price);

            Assert.Equal("Apple", list[8].Name);
            Assert.Equal(1.90, list[8].Price);
        }
    }
}
