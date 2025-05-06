using Microsoft.EntityFrameworkCore;
using ProductTrial.Data.Dtos;
using ProductTrial.Data.Entities;
using ProductTrial.Services.Interfaces;
using ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions;
using System.Text.Json;

namespace ProductTrial.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<ProductTrialDbContext> _contextFactory;

        public ProductService(IDbContextFactory<ProductTrialDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ProductDto> CreateAsync(ProductCreationDto dto)
        {
            try
            {
                Product newProduct = new Product(dto.Code, dto.Name, dto.Description, dto.Image, dto.Category, dto.Price, dto.Quantity, dto.InternalReference, dto.ShellId, dto.InventoryStatus, dto.Rating);
                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    Product? existingProduct = await context.Products.FirstOrDefaultAsync(f => f.Code == dto.Code);
                    if (existingProduct != null)
                    {
                        throw new AlreadyExistsException($"A product already exists for the code {dto.Code}.");
                    }

                    newProduct.CreatedAt = DateTimeOffset.UtcNow.Ticks;

                    await context.Products.AddAsync(newProduct);
                    int changeCount = await context.SaveChangesAsync();
                    if (changeCount == 0)
                    {
                        throw new DbUpdateException($"The given product can not be saved : {JsonSerializer.Serialize(newProduct)}");
                    }
                }
                return new ProductDto(newProduct.Id, newProduct.Code, newProduct.Name, newProduct.Description, newProduct.Image, newProduct.Category, newProduct.Price, newProduct.Quantity, newProduct.InternalReference, newProduct.ShellId, newProduct.InventoryStatus, newProduct.Rating);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                int changeCount;
                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    Product? existingProduct = await context.Products.FirstOrDefaultAsync(f => f.Id == id);
                    if (existingProduct == null)
                    {
                        throw new NotFoundException($"The given product does not exist (ID : {id}).");
                    }
                    context.Products.Remove(existingProduct);
                    changeCount = await context.SaveChangesAsync();
                }
                return changeCount > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            try
            {
                List<ProductDto> dst = new List<ProductDto>();
                List<Product> src;

                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    src = await context.Products.ToListAsync();
                }

                foreach (Product item in src)
                {
                    dst.Add(new ProductDto(item.Id, item.Code, item.Name, item.Description, item.Image, item.Category, item.Price, item.Quantity, item.InternalReference, item.ShellId, item.InventoryStatus, item.Rating));
                }
                return dst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            ProductDto dst;
            await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
            {
                Product? existingProduct = await context.Products.FirstOrDefaultAsync(f => f.Id == id);
                if (existingProduct == null)
                {
                    throw new NotFoundException($"The given product does not exist (ID : {id}).");
                }
                dst = new ProductDto(existingProduct.Id, existingProduct.Code, existingProduct.Name, existingProduct.Description, existingProduct.Image, existingProduct.Category, existingProduct.Price, existingProduct.Quantity, existingProduct.InternalReference, existingProduct.ShellId, existingProduct.InventoryStatus, existingProduct.Rating);
            }
            return dst;
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductUpdateDto editedProduct)
        {
            try
            {
                ProductDto dst;
                await using (ProductTrialDbContext context = _contextFactory.CreateDbContext())
                {
                    Product? existingProduct = await context.Products.FirstOrDefaultAsync(f => f.Id == id);

                    if (existingProduct == null)
                    {
                        throw new NotFoundException($"The given product does not exist (ID : {id}).");
                    }

                    existingProduct.Code = editedProduct.Code;
                    existingProduct.Name = editedProduct.Name;
                    existingProduct.Description = editedProduct.Description;
                    existingProduct.Image = editedProduct.Image;
                    existingProduct.Category = editedProduct.Category;
                    existingProduct.Price = editedProduct.Price;
                    existingProduct.Quantity = editedProduct.Quantity;
                    existingProduct.InternalReference = editedProduct.InternalReference;
                    existingProduct.ShellId = editedProduct.ShellId;
                    existingProduct.InventoryStatus = editedProduct.InventoryStatus;
                    existingProduct.Rating = editedProduct.Rating;
                    existingProduct.UpdatedAt = DateTimeOffset.UtcNow.Ticks;

                    context.Products.Update(existingProduct);
                    await context.SaveChangesAsync();
                    dst = new ProductDto(existingProduct.Id, existingProduct.Code, existingProduct.Name, existingProduct.Description, existingProduct.Image, existingProduct.Category, existingProduct.Price, existingProduct.Quantity, existingProduct.InternalReference, existingProduct.ShellId, existingProduct.InventoryStatus, existingProduct.Rating);
                }
                return dst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}