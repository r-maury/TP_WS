using FastMapper;
using TP_WebService.ModelsDto;
using TP_WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TP_WebService.Models.User;

namespace TP_WebService.Tools
{
    public class DtoTools
    {
        public static T2 Convert<T1, T2>(T1 objToConvert)
        {

            TypeAdapterConfig<Product, ProductDto>
                     .NewConfig()
                     .MapFrom(dest => dest.Id, src => src.Id)
                     .MapFrom(dest => dest.Description, src => src.Description)
                     .MapFrom(dest => dest.Price, src => src.Price);

            TypeAdapterConfig<ProductDto,Product>
                     .NewConfig()
                     .MapFrom(dest => dest.Id, src => src.Id)
                     .MapFrom(dest => dest.Description, src => src.Description)
                     .MapFrom(dest => dest.Price,src => src.Price);

            TypeAdapterConfig<User, UserDto>
                     .NewConfig()
                     .MapFrom(dest => dest.Name, src => src.Name)
                     .MapFrom(dest => dest.Email, src => src.Email)
                     .MapFrom(dest => dest.Password, src => src.Password)
                     .MapFrom(dest => dest.Role, src => src.Role.ToString());

            TypeAdapterConfig<UserDto, User>
                     .NewConfig()
                     .MapFrom(dest => dest.Name, src => src.Name)
                     .MapFrom(dest => dest.Email, src => src.Email)
                     .MapFrom(dest => dest.Password, src => src.Password)
                     .MapFrom(dest => dest.Role, src => (User.UserRole)Enum.Parse(typeof(User.UserRole), src.Role));

            var res = TypeAdapter.Adapt<T1, T2>(objToConvert);
            return res;
        }
    }
}