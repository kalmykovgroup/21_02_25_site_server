 
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.WishListEntity.DTOs;
using AutoMapper;
using Domain.Entities.OrderSpace;
using Domain.Entities.ProductSpace;

namespace Application.Handlers.ProductSpace.ProductEntity.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {


            // 🔹 Маппинг между сущностью `Product` и `ProductDto`
            CreateMap<Product, LongProductDto>();
            CreateMap<Product, ShortProductDto>();
            CreateMap<Product, WishListItemDto>();
             
             
             


        }
    }
}
