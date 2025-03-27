using AutoMapper;
using Products.DataAccess.Entities;
using ProductServices.DTOs.Product;

namespace ProductServices.Mapping
{
    public class ItemMapper
    {
    }

    public class ItemMapperProfile: Profile
    {
        public ItemMapperProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}
