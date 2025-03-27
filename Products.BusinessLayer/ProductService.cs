using Microsoft.AspNetCore.Http;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess;
using Products.DataAccess.Entities;

namespace Products.BusinessLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductsUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IProductsUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            // JWT API code is partially done, hence provided default value
            product.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "admin";
            product.CreatedOn = DateTime.UtcNow;
            product = await _unitOfWork.ProductRepository.CreateProduct(product);
            await _unitOfWork.SaveChanges();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            await _unitOfWork.ProductRepository.DeleteProduct(id);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _unitOfWork.ProductRepository.GetProduct(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _unitOfWork.ProductRepository.GetProducts();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            // JWT API code is partially done, hence provided default value
            product.ModifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "admin";
            product.ModifiedOn = DateTime.UtcNow;
            product = await _unitOfWork.ProductRepository.UpdateProduct(product);
            await _unitOfWork.SaveChanges();
            return product;
        }
    }
}
