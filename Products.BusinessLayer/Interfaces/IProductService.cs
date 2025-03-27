using Products.DataAccess.Entities;

namespace Products.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> UpdateProduct(Product product);
        Task<Product> CreateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
