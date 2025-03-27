using AutoMapper;
using DeepEqual.Syntax;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Products.BusinessLayer.Exceptions;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess.Entities;
using ProductServices.Controllers;
using ProductServices.DTOs.Product;

namespace ProductServices.Tests
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private ItemsController _controller;
        private IMapper _mapper;
        private IItemService _itemService;
        private ILogger<ItemsController> _logger;
        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _itemService = Substitute.For<IItemService>();
            _logger = Substitute.For<ILogger<ItemsController>>();
            _controller = new ItemsController(_mapper, _logger, _itemService);
        }

        [Test]
        public async Task GetItems_ValidData_OK()
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
            var items = new List<Item> {
                new Item {
                    Id = 1,
                    ProductId = 2,
                    Quantity = 10,
                    Product = product
                }};

            var expected = new ItemDto
            {
                Id = 1,
                ProductId = 2,
                Quantity = 20
            };

            _itemService.GetItems(Arg.Any<int>()).Returns(items);
            _mapper.Map<ItemDto>(Arg.Any<Item>()).Returns(expected);

            // Act
            var actual = await _controller.GetProductItems(1);

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }

        [Test]
        public void GetItems_InvalidProductId_ThrowsEntityNotFoundException()
        {
            // Arrange
            _itemService.GetItems(Arg.Any<int>()).Throws(
                new EntityNotFoundException("Product with id '100' does not exists."));

            // Assert
            var actual = _controller.GetProductItems(100);

            // Assert
            Assert.IsTrue(actual.Exception.InnerException is EntityNotFoundException);
            Assert.AreEqual("Product with id '100' does not exists.", actual.Exception.InnerException.Message);
        }

        // Optional
        //[TearDown]
        //public void Cleanup() {  }
    }
}
