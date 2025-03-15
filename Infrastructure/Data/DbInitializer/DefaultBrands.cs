 
using Domain.Entities.BrandSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultBrands
{
    public static List<Brand> CreateDefaultBrands(this ModelBuilder modelBuilder, Guid createdByUserId)
    {
          List<Brand> brands = new List<Brand>()
            {
                new Brand { Id = Guid.NewGuid(), Name = "Apple", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Blackview", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "BQ", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Google", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "HONOR", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "HUAWEI", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Infinix", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "iQOO", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Itel", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Nothing", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Oneplus", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Oppo", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Realme", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Samsung", CreatedByUserId = createdByUserId }, 
                new Brand { Id = Guid.NewGuid(), Name = "STARLET", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "TECNO", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Ulefone", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Vivo", CreatedByUserId = createdByUserId },
                new Brand { Id = Guid.NewGuid(), Name = "Xiaomi", CreatedByUserId = createdByUserId },
            };

            modelBuilder.Entity<Brand>().HasData(brands);

            return brands;
    }
}