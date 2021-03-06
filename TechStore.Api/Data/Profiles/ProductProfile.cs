using System;
using AutoMapper;
using TechStore.Api.Data.Enteties;
using TechStore.Models.Models;

namespace TechStore.Api.Data.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();

            CreateMap<ProductModel, Product>()
                .ForMember(m => m.Id, src => src.Ignore());
        }
    }
}
