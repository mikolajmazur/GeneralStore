using AutoMapper;
using GeneralStore.Api.Queries;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;

namespace GeneralStore.Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductCommand, Product>();
        }
    }
}
