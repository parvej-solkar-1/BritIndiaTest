using AutoMapper;
using DeepEqual.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess.Entities;
using ProductServices.Controllers;
using ProductServices.DTOs.Product;

namespace ProductServices.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private IMapper _mapper;
        private IProductService _productService;
        private ILogger<ProductsController> _logger;
        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _productService = Substitute.For<IProductService>();
            _logger = Substitute.For<ILogger<ProductsController>>();
            _controller = new ProductsController(_mapper, _productService, _logger);
        }

        [Test]
        public async Task CreateProduct_ValidData_OK()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var userName = "admin";
            var product = new Product
            {
                Id = 1,
                ProductName = "Product 1",
                CreatedBy = userName,
                CreatedOn = dateTime
            };
            var expected = new ProductDto
            {
                Id = 1,
                ProductName = "Product 1",
                CreatedBy = userName,
                CreatedOn = dateTime
            };

            _mapper.Map<Product>(Arg.Any<ProductDto>()).Returns(new Product());
            _productService.CreateProduct(Arg.Any<Product>()).Returns(product);
            _mapper.Map<ProductDto>(Arg.Any<Product>()).Returns(expected);

            // Act
            var actual = ((await _controller.CreateProduct(new ProductDto())) as ObjectResult).Value;

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }

        [Test]
        public void CreateProduct_NullPayload_ThrowsArgumentException()
        {
            // Act
            // Assert
            var actual = _controller.CreateProduct(null);

            // Assert
            Assert.IsTrue(actual.Exception.InnerException is ArgumentException);
            Assert.AreEqual("Request payload is required.", actual.Exception.InnerException.Message);
        }

        // Optional
        //[TearDown]
        //public void Cleanup() {  }
    }
}
