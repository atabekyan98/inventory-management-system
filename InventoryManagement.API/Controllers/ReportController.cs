using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        [HttpGet("export-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExportProducts()
        {
            try
            {
                var reportBytes = await _reportService.GenerateProductsReportAsync();
                return File(reportBytes, "text/csv", "products.csv");
            }
            catch (Exception ex)
            {
                // Log the error (you should inject ILogger in production)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating the report");
            }
        }
    }
}