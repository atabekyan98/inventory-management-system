using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Infrastructure.Entities;

namespace InventoryManagement.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();
        }
    }
}