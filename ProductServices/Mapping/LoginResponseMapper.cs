using AutoMapper;
using Products.DataAccess.Models;
using ProductServices.DTOs.Login;
//using ProductServices.src.Domain.Entities.Product;

namespace ProductServices.Mapping
{
    public class LoginResponseMapper
    {
        
    }

    public class LoginResponseMapperProfile: Profile
    {
        public LoginResponseMapperProfile()
        {
            CreateMap<LoginResponse, LoginResponseDto>();
        }
    }
}
