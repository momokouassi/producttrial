using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTrial.Data.Dtos;
using ProductTrial.Data.Entities;
using ProductTrial.Services.Interfaces;

namespace ProductTrial.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Created product</returns>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] ProductCreationDto product)
        {
            ProductDto res = await _productService.CreateAsync(product);
            return Ok(res);
        }

        /// <summary>
        /// Remove a given product
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <returns>Deleted product ID</returns>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            bool res = await _productService.DeleteAsync(id);
            return Ok(res);
        }

        /// <summary>
        /// Retrieve all products
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllAsync()
        {
            List<ProductDto> res = await _productService.GetAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Retrieve details for a given product
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <returns>Corresponding product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(int id)
        {
            ProductDto res = await _productService.GetAsync(id);
            return Ok(res);
        }

        /// <summary>
        /// Update details of product 1 if it exists
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <param name="product">Details to save</param>
        /// <returns>Edited product</returns>
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateAsync(int id, [FromBody] ProductUpdateDto dto)
        {
            ProductDto res = await _productService.UpdateAsync(id, dto);
            return Ok(res);
        }
    }
}