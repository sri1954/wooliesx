using AppCore.Interfaces;
using AppCore.Models;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;
using XUnitTestProject.Helper;

namespace XUnitTestProject
{
    public class WebAPI_ProductsController_UnitTests
    {
        private Mock<IWooliesRepository<Product>> _mockRepository;
        private ProductsController _controller;
        private string strToken = "e3b5361b-e85c-4c62-85fd-4c9e2af2c053";

        public WebAPI_ProductsController_UnitTests()
        {
            // Arrange
            _mockRepository = new Mock<IWooliesRepository<Product>>();
        }

        [Fact]
        public async Task HttpGet_GetProducts_WhenCalled_ReturnsOKResult()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            var okObjectResult = await _controller.Get(strToken);

            // Assert
            Assert.IsType<OkObjectResult>((OkObjectResult)okObjectResult.Result);
        }

        [Fact]
        public async Task HttpGet_GetProducts_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            var okObjectResult = await _controller.Get(strToken);
            var result = (OkObjectResult)okObjectResult.Result;

            // Assert
            // mock product list have exactly five products. 
            // test should pass
            var items = (List<Product>)result.Value;
            Assert.Equal(9, items.Count);
        }

        [Fact]
        public async Task HttpGet_GetProduct_WhenIncorrectProductIdPassed_ReturnsNotFoundOKResult()
        {
            // Arrange
            int ProductId = 100;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
                  .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list does not have a product with productId 100
            // should throw not found response
            var notFoundResult = await _controller.Get(100);

            // Assert
            Assert.IsType<NotFoundResult>((NotFoundResult)notFoundResult.Result);
        }

        [Fact]
        public async Task HttpGet_GetProduct_WhenCorrectProductIdPassed_ReturnsOKResult()
        {
            // Arrange
            int ProductId = 5;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
                  .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list has a product with productid 5 and name 'Coffee'
            var okObjectResult = await _controller.Get(5);

            // Assert
            Assert.IsType<OkObjectResult>((OkObjectResult)okObjectResult.Result);
        }

        [Fact]
        public async Task HttpGet_GetProduct_WhenCorrectProductIdPassed_ReturnsProductType()
        {
            // Arrange
            int ProductId = 5;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
                  .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list has a product with productid 5 and name 'Coffee'
            var okObjectResult = await _controller.Get(5);
            var result = (OkObjectResult)okObjectResult.Result;

            // Assert
            var items = (Product)result.Value;
            Assert.Equal("Apple", items.Name);
        }

        [Fact]
        public async Task HttpPost_PostProduct_WhenInvalidNamePassed_ReturnsBadRequest()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // product name is required not null or empty
            // should return bad request
            Product product = new Product
            {
                ProductId = 0,
                Name = "",
                Price = 10,
                Quantity = 1
            };

            var badRequestResult = await _controller.Post(product);
            var result = (BadRequestObjectResult)badRequestResult.Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task HttpPost_PostProduct_WhenInvalidPricePassed_ReturnsBadRequest()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // product price must be greater than zero
            // should return bad request
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test Product",
                Price = 0,
                Quantity = 1
            };

            var badRequestResult = await _controller.Post(product);
            var result = (BadRequestObjectResult)badRequestResult.Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task HttpPost_PostProduct_WhenInvalidQuantityPassed_ReturnsBadRequest()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // product quantity must be greater than zero
            // should return bad request
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test Product",
                Price = 10,
                Quantity = 0
            };

            var badRequestResult = await _controller.Post(product);
            var result = (BadRequestObjectResult)badRequestResult.Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task HttpPost_PostProduct_WhenValidObjectPassed_ReturnsOKRequest()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(FakeProduct.GetProducts());
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // passing valid product object should return ok request
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test Product",
                Price = 10,
                Quantity = 1
            };

            var okObjectResult = await _controller.Post(product);
            var result = (OkObjectResult)okObjectResult.Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var item = (Product)result.Value;
            Assert.Equal("Test Product", item.Name);
            Assert.Equal(10, item.Price);
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public async Task HttpPut_PutProduct_WhenInvalidProductIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            int ProductId = 1000;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
            .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));

            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list does not have a product with productid 1000
            // passing invalid productid should return not found
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test Product",
                Price = 10,
                Quantity = 1
            };

            var badRequestResult = await _controller.Put(ProductId, product);
            var result = (NotFoundObjectResult)badRequestResult.Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task HttpPut_PutProduct_WhenValidProductIdPassed_ReturnsOKResult()
        {
            // Arrange
            int ProductId = 5;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
            .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));
            _controller = new ProductsController(_mockRepository.Object);

            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list has a product with productid 5
            // passing valid productid should return ok request
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test product five update",
                Price = 107,
                Quantity = 12
            };

            var okObjectResult = await _controller.Put(ProductId, product);
            var result = (OkObjectResult)okObjectResult.Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var item = (Product)result.Value;
            Assert.Equal("Test product five update", item.Name);
            Assert.Equal(107, item.Price);
            Assert.Equal(12, item.Quantity);
        }

        [Fact]
        public async Task HttpDelete_DeleteProduct_WhenInvalidProductIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            int ProductId = 1000;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
            .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));

            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list does not have a product with productid 1000
            // passing invalid productid should return not found
            Product product = new Product
            {
                ProductId = 0,
                Name = "Test Product",
                Price = 10,
                Quantity = 1
            };

            var badRequestResult = await _controller.Delete(ProductId);
            var result = (NotFoundObjectResult)badRequestResult.Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task HttpDelete_DeleteProduct_WhenValidProductIdPassed_ReturnsOKResult()
        {
            // Arrange
            int ProductId = 5;
            _mockRepository.Setup(repo => repo.GetById(ProductId))
            .ReturnsAsync(FakeProduct.GetProducts().FirstOrDefault(p => p.ProductId == ProductId));
            _controller = new ProductsController(_mockRepository.Object);

            // Act
            // mock product list has a product with productid 5
            // passing valid productid should return ok request
            var okObjectResult = await _controller.Delete(ProductId);
            var result = (OkObjectResult)okObjectResult.Result;

            // Assert
            Assert.Equal(true, result.Value);
        }
    }
}
