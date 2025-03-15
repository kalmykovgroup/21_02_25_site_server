using Domain.Entities.CategorySpace;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultSellerOffers
{
    public static List<SellerOffer> CreateDefaultSellerOffers(
        this ModelBuilder modelBuilder, 
        (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations,
        List<CategoryAttribute> categoryAttributes,
        List<CategoryAttributeValue> categoryAttributeValues,
        List<Product> products, 
        Seller seller,
        Guid createdByUserId)
    {
        List<SellerOffer> sellerOffers = new List<SellerOffer>();
        
        
        modelBuilder.Entity<SellerOffer>().HasData(sellerOffers);
        return sellerOffers;
        
    }
    
    // Метод генерации цены в зависимости от модели и памяти
    private static decimal GeneratePrice(string productName, string storage)
    {
        decimal basePrice = productName.Contains("Ultra") || productName.Contains("Pro Max") ? 1199.99m :
            productName.Contains("Pro") || productName.Contains("+") ? 999.99m : 799.99m;

        return storage switch
        {
            "128GB" => basePrice,
            "256GB" => basePrice + 100m,
            "512GB" => basePrice + 200m,
            "1TB" => basePrice + 400m,
            _ => basePrice
        };
    }
}