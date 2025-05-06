using ProductTrial.Data.Dtos;
using ProductTrial.Data.Entities;

namespace ProductTrial.Services.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Created product</returns>
        Task<ProductDto> CreateAsync(ProductCreationDto product);

        /// <summary>
        /// Remove a given product
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <returns>Deleted product ID</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieve all products
        /// </summary>
        /// <returns>Products</returns>
        Task<List<ProductDto>> GetAllAsync();

        /// <summary>
        /// Retrieve details for a given product
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <returns>Corresponding product</returns>
        Task<ProductDto> GetAsync(int id);

        /// <summary>
        /// Update details of product 1 if it exists
        /// </summary>
        /// <param name="id">Given product ID</param>
        /// <param name="product">Details to save</param>
        /// <returns>Edited product</returns>
        Task<ProductDto> UpdateAsync(int id, ProductUpdateDto product);
    }
}