using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Services;
using InventoryManagement.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productService.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProduct),
                new { id = createdProduct.Id },
                _mapper.Map<ProductDto>(createdProduct));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateProductAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpPost("adjust-stock")]
        public async Task<IActionResult> AdjustStock([FromBody] StockAdjustmentDto adjustment)
        {
            if (!Enum.TryParse(adjustment.TransactionType, out TransactionType transactionType))
                return BadRequest("Invalid transaction type");

            try
            {
                await _productService.AdjustStockAsync(
                    adjustment.ProductId,
                    adjustment.Quantity,
                    transactionType,
                    adjustment.Notes);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}