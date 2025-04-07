using FluentAssertions;
using InventoryManagement.Core.Services;
using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace InventoryManagement.Tests.Unit
{
    public class ProductServiceTests
    {
        private readonly DbContextOptions<InventoryDbContext> _dbContextOptions;
        private readonly Mock<ILogger<ProductService>> _mockLogger;

        public ProductServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockLogger = new Mock<ILogger<ProductService>>();
        }

        [Fact]
        public async Task CreateProductAsync_ShouldAddProductToDatabase()
        {
            // Arrange
            using var context = new InventoryDbContext(_dbContextOptions);
            var unitOfWork = new UnitOfWork(context);
            var service = new ProductService(unitOfWork, _mockLogger.Object);

            var product = new Product
            {
                Name = "Test Product",
                Description = "Test product for unit test",
                Price = 9.99m,
                StockQuantity = 10,
                CategoryId = 1,
                SupplierId = 1
            };

            // Act
            var result = await service.CreateProductAsync(product);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);

            var dbProduct = await context.Products.FindAsync(result.Id);
            dbProduct.Should().NotBeNull();
            dbProduct.Name.Should().Be("Test Product");
        }

        [Fact]
        public async Task AdjustStockAsync_ShouldIncreaseStock_ForPurchaseTransaction()
        {
            // Arrange
            using var context = new InventoryDbContext(_dbContextOptions);

            // Add a product first
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test product for unit test",
                Price = 9.99m,
                StockQuantity = 10,
                CategoryId = 1,
                SupplierId = 1
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var service = new ProductService(unitOfWork, _mockLogger.Object);

            // Act
            await service.AdjustStockAsync(product.Id, 5, TransactionType.Purchase, "Test purchase");

            // Assert
            var updatedProduct = await context.Products.FindAsync(product.Id);
            updatedProduct.StockQuantity.Should().Be(15);

            var transaction = await context.Transactions.FirstOrDefaultAsync();
            transaction.Should().NotBeNull();
            transaction.Quantity.Should().Be(5);
            transaction.Type.Should().Be(TransactionType.Purchase);
        }
    }
}