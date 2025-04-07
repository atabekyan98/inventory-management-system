using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AdjustStockAsync(int productId, int quantity, TransactionType type, string notes = null)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            if (type == TransactionType.Purchase)
            {
                product.StockQuantity += quantity;
            }
            else if (type == TransactionType.Sale)
            {
                if (product.StockQuantity < quantity)
                    throw new InvalidOperationException("Insufficient stock for this transaction.");

                product.StockQuantity -= quantity;
            }

            var transaction = new Transaction
            {
                ProductId = productId,
                Quantity = quantity,
                Type = type,
                Notes = notes,
                TransactionDate = DateTime.UtcNow
            };

            _unitOfWork.Products.Update(product);
            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
        }
    }
}