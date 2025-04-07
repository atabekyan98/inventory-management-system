using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Core.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto> GetByIdAsync(int id);
        Task<SupplierDto> CreateAsync(SupplierDto supplierDto);
        Task UpdateAsync(int id, SupplierDto supplierDto);
        Task DeleteAsync(int id);
    }
}
