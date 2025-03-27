using Microsoft.EntityFrameworkCore;
using Products.DataAccess.Entities;

namespace Products.DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ProductDbContext _context;
        public ItemRepository(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetItems(int productId)
        {
            return await _context.Items.Where(x => x.ProductId == productId)
                .AsNoTracking().ToListAsync();
        }
    }
}
