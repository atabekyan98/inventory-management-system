using CsvHelper;
using InventoryManagement.Infrastructure.Data;
using System.Globalization;

namespace InventoryManagement.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> GenerateProductsReportAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(products);
            await streamWriter.FlushAsync();

            return memoryStream.ToArray();
        }
    }
}
