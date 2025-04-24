 
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.WishListEntity.DTOs;
using AutoMapper;
using Domain.Entities.OrderSpace;
using Domain.Entities.ProductSpace;

namespace Application.Handlers.ProductSpace.ProductEntity.Mapping
{
    public class ProductProfile : Profile
    {
        private const string BaseImageUrl = "https://cdn.example.com/images/products";
        public ProductProfile()
        {


            // 🔹 Маппинг между сущностью `Product` и `ProductDto` 
            CreateMap<SellerOffer, ShortSellerOfferDto>();
            CreateMap<SellerOffer, WishListItemDto>();
            
            CreateMap<SellerOfferImage, SellerOfferImageDto>()
                .ForMember(dest => dest.Urls, opt => opt.MapFrom(src => GenerateImageUrls(src)));


            

            CreateMap<RecommendedGroup, RecommendedGroupDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom((src, dest, destMember, context) =>
                src.RecommendedGroupProducts.Select(rgp =>
                    context.Mapper.Map<ShortSellerOfferDto>(rgp.Product) // ✅ Автоматически маппит `Product → ShortProductDto`
                )
            ));


        }
        
        private static Dictionary<string, string> GenerateImageUrls(SellerOfferImage img)
        {
            return new Dictionary<string, string>
            {
                ["100x100"] = $"{BaseImageUrl}/{img.StoragePath}/100x100.{img.OriginalExtension}",
                ["200x200"] = $"{BaseImageUrl}/{img.StoragePath}/200x200.{img.OriginalExtension}",
                ["400x400"] = $"{BaseImageUrl}/{img.StoragePath}/400x400.{img.OriginalExtension}",
                ["800x800"] = $"{BaseImageUrl}/{img.StoragePath}/800x800.{img.OriginalExtension}",
                ["1200x1200"] = $"{BaseImageUrl}/{img.StoragePath}/1200x1200.{img.OriginalExtension}",
                ["original"] = $"{BaseImageUrl}/{img.StoragePath}/original.{img.OriginalExtension}"
            };
        }
    }
}
