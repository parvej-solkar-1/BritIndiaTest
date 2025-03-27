using AutoMapper;
using Products.DataAccess.Entities;
using ProductServices.DTOs.Product;

namespace ProductServices.Mapping
{
    public class ProductMapper
    {
    }

    public class ProductMapperProfile: Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<Product, ProductDto>();
        }
    }
}
