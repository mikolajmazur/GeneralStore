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
            CreateMap<Product, ProductSimplifiedDto>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(source => source.CurrentStatus.ToString()));
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
