using Domain.Entities.CategorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultSellerOffers
{
    
    private static Random random = new Random();
    
    public static List<SellerOffer> CreateDefaultSellerOffers(
        this ModelBuilder modelBuilder, 
        List<(Product product, List<ProductVariant> productVariants, List<ProductVariantAttributeValue> productVariantAttributeValues)> productVariants,
        List<CategoryAttribute> categoryAttributes,
        Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues, 
        Seller seller,
        Guid createdByUserId)
    {
        List<SellerOffer> sellerOffers = new List<SellerOffer>();

        foreach (var item in productVariants)
        {
            foreach (var productVariant in item.productVariants)
            {

                List<ProductVariantAttributeValue> productVariantAttributeValues =
                    item.productVariantAttributeValues.Where(e => e.ProductVariantId == productVariant.Id).ToList();
                   
                
                if(productVariantAttributeValues == null) throw new Exception("ProductVariantAttributeValues not found");
                
                string? memory = null;
                string? color = null;

                foreach (ProductVariantAttributeValue productVariantAttributeValue in productVariantAttributeValues)
                {
                    CategoryAttribute? _categoryAttribute =
                        categoryAttributes
                            .Where(c => c.Id == productVariantAttributeValue.CategoryAttributeId).FirstOrDefault();
                    
                    CategoryAttributeValue? _categoryAttributeValue = categoryAttributeValues[_categoryAttribute.Id]
                        .Where(cav => cav.Id == productVariantAttributeValue.CategoryAttributeValueId).FirstOrDefault();
                    
                    if (_categoryAttribute == null) throw new Exception("CategoryAttribute not found");
                    if (_categoryAttributeValue == null) throw new Exception("CategoryAttributeValue not found");
                    
                    if(_categoryAttribute.Name == "Память")
                    {
                        memory = _categoryAttributeValue.Value;
                    }
                    else if(_categoryAttribute.Name == "Цвет")
                    {
                        color = _categoryAttributeValue.Value;
                    }
                     
                }
                    
                
                sellerOffers.Add(
                    new SellerOffer()
                    {
                        Id = Guid.NewGuid(),
                        ProductVariantId = productVariant.Id,
                        Name = $"{item.product.Name} {color} {memory}",
                        SellerId = seller.Id,
                        Price =  random.Next(50000, 200000),
                        IsActive = true,
                        CreatedByUserId = createdByUserId, 
                        StockQuantity = random.Next(1, 30),
                    }
                );
            }
          
        }
        
        modelBuilder.Entity<SellerOffer>().HasData(sellerOffers);
        return sellerOffers;
        
    }
    
   
}