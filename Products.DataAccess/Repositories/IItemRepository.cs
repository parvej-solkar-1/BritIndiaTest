using Products.DataAccess.Entities;

namespace Products.DataAccess.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItems(int productId);
    }
}
