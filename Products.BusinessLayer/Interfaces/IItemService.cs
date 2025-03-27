using Products.DataAccess.Entities;

namespace Products.BusinessLayer.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItems(int productId);
    }
}
