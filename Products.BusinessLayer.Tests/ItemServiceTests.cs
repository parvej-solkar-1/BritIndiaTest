using DeepEqual.Syntax;
using NSubstitute;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess;
using Products.DataAccess.Entities;

namespace Products.BusinessLayer.Tests
{
    public class ItemServiceTests
    {
        private ItemService _itemService;
        private IProductsUnitOfWork _unitOfWork;
        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IProductsUnitOfWork>();
            _productService = Substitute.For<IProductService>();
            _itemService = new ItemService(_unitOfWork, _productService);
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
            var expected = new List<Item> {
                new Item {
                    Id = 1,
                    ProductId = 2,
                    Quantity = 10,
                    Product = product
                }};

            _productService.GetProduct(Arg.Any<int>()).Returns(product);
            _unitOfWork.ItemRepository.GetItems(Arg.Any<int>()).Returns(items);

            // Act
            var actual = await _itemService.GetItems(1);

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }
    }
}
