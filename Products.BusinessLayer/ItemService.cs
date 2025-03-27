using Products.BusinessLayer.Exceptions;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess;
using Products.DataAccess.Entities;

namespace Products.BusinessLayer
{
    public class ItemService : IItemService
    {
        private readonly IProductsUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public ItemService(IProductsUnitOfWork unitOfWork, IProductService productService)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Item>> GetItems(int productId)
        {
            var product = _productService.GetProduct(productId);
            if (product == null)
                throw new EntityNotFoundException($"Product with id '{productId}' does not exists."); ;
            return await _unitOfWork.ItemRepository.GetItems(productId);
        }
    }
}
