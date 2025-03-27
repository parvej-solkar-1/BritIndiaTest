using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.BusinessLayer.Exceptions;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess.Entities;
using ProductServices.DTOs.Product;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "admin")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IMapper mapper,
            IProductService productService,
            ILogger<ProductsController> logger)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <response code="200">Fetched all products successfully</response>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogTrace("START: Reading using Product service");
            var productEntities = await _productService.GetProducts();
            _logger.LogTrace("END: Reading using service methed");
            var products = _mapper.Map<List<ProductDto>>(productEntities);
            return Ok(products);
        }

        /// <summary>
        /// Get a specific product by unique id
        /// </summary>
        /// <remarks>Product id needs to be valid and existing</remarks>
        /// <response code="200">Product created</response>
        /// <response code="404">Product id does not exists</response>
        /// <param  name="id">Product Id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            _logger.LogTrace("START: Reading using Product service");
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"ERROR: Product with id '{id}' does not exists");
                throw new EntityNotFoundException($"Product with id '{id}' does not exists.");
            }

            _logger.LogTrace("END: Reading using Product service");
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Updates a specific product by unique id
        /// </summary>
        /// <remarks>Product id needs to be valid and existing</remarks>
        /// <response code="200">Product created</response>
        /// <response code="400">Bad/ Invalid input by the user</response>
        /// <response code="404">Product id does not exists</response>
        /// <response code="500">Something wrong at server side</response>
        /// <param  name="id">Product Id</param>
        /// <param name="productDto">Product details</param>
        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id <= 0)
                throw new ArgumentException($"Product with id '{id}' does not exists.");

            if (productDto == null)
                throw new ArgumentException("Request payload is required.");

            _logger.LogTrace("START: Reading data using Product service");
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"ERROR: Product with id '{id}' does not exists.");
                throw new EntityNotFoundException($"Product with id '{id}' does not exists.");
            }

            _logger.LogTrace("END: Reading data using Product service");
            _mapper.Map(productDto, product);
            _logger.LogTrace("START: Updating data using Product service");
            product = await _productService.UpdateProduct(product);
            _logger.LogTrace("END: Updating data using Product service");
            _logger.LogInformation($"Product updated with id '{productDto.Id}'");
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <response code="200">Product created</response>
        /// <response code="400">Bad/ Invalid input by the user</response>
        /// <response code="500">Something wrong at server side</response>
        /// <param name="productDto">Product details</param>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentException("Request payload is required.");

            var product = _mapper.Map<Product>(productDto);

            _logger.LogTrace("START: Creating data using Product service");
            product = await _productService.CreateProduct(product);
            _logger.LogTrace("END: Creating data using Product service");

            var newProductDto = _mapper.Map<ProductDto>(product);
            _logger.LogInformation($"Product created with id '{productDto.Id}'");
            return CreatedAtAction("GetProduct", new { id = newProductDto.Id }, newProductDto);
        }

        /// <summary>
        /// Delete a specific product by unique id
        /// </summary>
        /// <remarks>Product id needs to be valid and existing</remarks>
        /// <response code="200">Product created</response>
        /// <response code="400">Bad/ Invalid input by the user</response>
        /// <response code="404">Product id does not exists</response>
        /// <response code="500">Something wrong at server side</response>
        /// <param  name="id">Product Id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Product with id '{id}' does not exists.");

            _logger.LogTrace("START: Deleting data using Product service");
            var isDeleteSuccessful = await _productService.DeleteProduct(id);
            _logger.LogTrace("END: Deleting data using Product service");
            if (!isDeleteSuccessful)
            {
                _logger.LogError($"ERROR: Product with id '{id}' does not exists.");
                throw new EntityNotFoundException($"Product with id '{id}' does not exists.");
            }

            _logger.LogInformation($"Product deleted with id '{id}'");
            return NoContent();
        }
    }
}
