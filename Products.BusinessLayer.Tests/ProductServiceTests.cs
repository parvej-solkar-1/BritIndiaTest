using DeepEqual.Syntax;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Products.DataAccess;
using Products.DataAccess.Entities;

namespace Products.BusinessLayer.Tests
{
    public class ProductServiceTests
    {
        private ProductService _productService;
        private IProductsUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IProductsUnitOfWork>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _productService = new ProductService(_unitOfWork, _httpContextAccessor);
        }

        [Test]
        public async Task GetProduct_ValidData_OK()
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
            var expected = new Product
            {
                Id = 1,
                ProductName = "Product 1",
                CreatedBy = userName,
                CreatedOn = dateTime
            };

            _httpContextAccessor.HttpContext.User.Identity.Name.Returns("admin");
            _unitOfWork.ProductRepository.CreateProduct(Arg.Any<Product>()).Returns(product);

            // Act
            var actual = await _productService.CreateProduct(new Product());

            // Assert
            Assert.IsTrue(actual.IsDeepEqual(expected));
        }
    }
}
