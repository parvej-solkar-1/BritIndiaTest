using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.BusinessLayer.Interfaces;
using ProductServices.DTOs.Product;

namespace ProductServices.Controllers
{
    [Route("api/products/{productId}/[controller]")]
    //[Authorize]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(
            IMapper mapper,
            ILogger<ItemsController> logger,
            IItemService itemService)
        {
            _mapper = mapper;
            _logger = logger;
            _itemService = itemService;
        }

        /// <summary>
        /// Get items of a specific product by unique id
        /// </summary>
        /// <remarks>Product id needs to be valid and existing</remarks>
        /// <response code="200">Product created</response>
        /// <response code="404">Product id does not exists</response>
        /// <param  name="id">Product Id</param>
        [HttpGet]
        public async Task<IActionResult> GetProductItems(int productId)
        {
            _logger.LogTrace("START: Reading using Item service");
            var itemEntities = await _itemService.GetItems(productId);
            _logger.LogTrace("END: Reading using Item service");
            var items = _mapper.Map<List<ItemDto>>(itemEntities);
            return Ok(items);
        }
    }
}
