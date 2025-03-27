using Products.DataAccess.Repositories;

namespace Products.DataAccess
{
    public class ProductsUnitOfWork : IProductsUnitOfWork
    {
        private readonly IProductRepository _productRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ProductDbContext _context;

        public IProductRepository ProductRepository { get { return _productRepository; } }
        public IItemRepository ItemRepository { get { return _itemRepository; } }

        public ProductsUnitOfWork(ProductDbContext context, IProductRepository productRepository, 
            IItemRepository itemRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _itemRepository = itemRepository;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
