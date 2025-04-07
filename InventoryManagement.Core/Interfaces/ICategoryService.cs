using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
        Task UpdateAsync(int id, CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}
