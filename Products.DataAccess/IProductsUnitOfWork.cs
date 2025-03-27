using Products.DataAccess.Repositories;

namespace Products.DataAccess
{
    public interface IProductsUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IItemRepository ItemRepository { get; }
        Task SaveChanges();
    }
}
