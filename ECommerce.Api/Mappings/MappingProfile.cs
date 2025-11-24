using AutoMapper;
using ECommerce.Api.Dtos.Product;
using ECommerce.Api.Entities;

namespace ECommerce.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
        }
    }
}
