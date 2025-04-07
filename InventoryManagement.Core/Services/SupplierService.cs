using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Interfaces;
using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Entities;

namespace InventoryManagement.Core.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> CreateAsync(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _unitOfWork.Suppliers.AddAsync(supplier);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task UpdateAsync(int id, SupplierDto supplierDto)
        {
            var existingSupplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            _mapper.Map(supplierDto, existingSupplier);
            _unitOfWork.Suppliers.Update(existingSupplier);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            _unitOfWork.Suppliers.Remove(supplier);
            await _unitOfWork.CompleteAsync();
        }
    }
}
