using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultSellers
{
    public static Seller CreateSellers(this ModelBuilder modelBuilder, Guid createdByUserId)
    {
                          
        var sellers = new List<Seller>
        {
           new Seller(){
                   Id = Guid.NewGuid(),
                   Name = "Kalmykov Group",
                   IsActive = true,
                   Rating = 5.0m,
                   CreatedByUserId = createdByUserId,
           }
        };

        modelBuilder.Entity<Seller>().HasData(sellers);
        
        return sellers.First();
    }
}