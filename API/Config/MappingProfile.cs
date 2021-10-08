using API.Models;
using AutoMapper;
using Domain.Entities;

namespace API.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDisplay>();
            CreateMap<ProductDisplay, Product>();


            CreateMap<Product, ProductInventory>();
            CreateMap<ProductInventory, Product>()
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
                .ForMember(dest => dest.IsOutOfStock, opt => opt.Ignore());
        }
    }
}