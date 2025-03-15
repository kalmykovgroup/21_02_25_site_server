using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.SupplierSpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultSuppliers
{
    public static Supplier CreateDefaultSuppliers(this ModelBuilder modelBuilder, Guid createdByUserId)
    {
        // Поставщики
        List<Supplier> suppliers = new List<Supplier>()
        {
            new Supplier { Id = Guid.NewGuid(), Name = "Default", CreatedByUserId = createdByUserId },
            new Supplier { Id = Guid.NewGuid(), Name = "Южные ворота", CreatedByUserId = createdByUserId },
            new Supplier { Id = Guid.NewGuid(), Name = "РМС", CreatedByUserId = createdByUserId }
        };
        modelBuilder.Entity<Supplier>().HasData(suppliers);

        // Адреса поставщиков
        modelBuilder.Entity<SupplierAddress>().HasData(
            new SupplierAddress { Id = Guid.NewGuid(), SupplierId = suppliers[1].Id, Street = "МКАД, 19-й километр, вл20с1", City = "Москва", PostalCode = "101000", Country = "Россия", IsPrimary = true, CreatedByUserId = createdByUserId },
            new SupplierAddress { Id = Guid.NewGuid(), SupplierId = suppliers[2].Id, Street = "ул. Калинина, д. 5", City = "Санкт-Петербург", PostalCode = "190000", Country = "Россия", IsPrimary = true, CreatedByUserId = createdByUserId },
            new SupplierAddress { Id = Guid.NewGuid(), SupplierId = suppliers[2].Id, Street = "ул. Жукова, д. 8", City = "Новосибирск", PostalCode = "630000", Country = "Россия", IsPrimary = false, CreatedByUserId = createdByUserId },
            new SupplierAddress { Id = Guid.NewGuid(), SupplierId = suppliers[2].Id, Street = "ул. Тверская, д. 40", City = "Екатеринбург", PostalCode = "620000", Country = "Россия", IsPrimary = false, CreatedByUserId = createdByUserId }
        );

        return suppliers.Where(s => s.Name == "Default").First();
    }
}