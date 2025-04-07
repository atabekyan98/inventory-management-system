using InventoryManagement.Infrastructure.Entities;

namespace InventoryManagement.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task AdjustStockAsync(int productId, int quantity, TransactionType type, string notes = null);
    }
}