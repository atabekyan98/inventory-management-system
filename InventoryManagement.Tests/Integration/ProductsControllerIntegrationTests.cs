using InventoryManagement.Core.Dtos;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove any existing DbContext registration (e.g., for SQL Server)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<InventoryDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register InMemory database for testing
                services.AddDbContext<InventoryDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestDb");
                });
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedResponse()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Name = "Integration Test Product",
            Description = "Test Description",
            Price = 19.99m,
            StockQuantity = 20,
            CategoryId = 1,
            SupplierId = 1
        };

        var content = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdProduct = JsonConvert.DeserializeObject<ProductDto>(responseContent);
        createdProduct.Should().NotBeNull();
        createdProduct.Id.Should().BeGreaterThan(0);
        createdProduct.Name.Should().Be(productDto.Name);
    }
}
