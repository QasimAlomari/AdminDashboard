using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper CreateMapper()
        {
            var MapConfig = new MapperConfiguration(x =>
            {
                
                x.CreateMap<Category,CategoryModel>().ReverseMap();
                x.CreateMap<Category, CategoryModelId>().ReverseMap();
                x.CreateMap<Category, CategoryModelCreate>().ReverseMap();


                

                //x.CreateMap<Category, CategoryModelId>().ForMember(dest => dest.Id,
                //    opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();
               


                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////

                x.CreateMap<Product, ProductModel>().ReverseMap();
                x.CreateMap<Product, ProductModelId>().ReverseMap();
                x.CreateMap<Product, ProductModelCreate>().ReverseMap();
                x.CreateMap<Product, ProductModelUpdate>().ReverseMap();

                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                
                x.CreateMap<User, UserModel>().ReverseMap();
                x.CreateMap<User, UserLoginModel>().ReverseMap();
                x.CreateMap<User, UserRegisterModel>().ReverseMap();


            });

            return MapConfig.CreateMapper();
        }
    }
}
