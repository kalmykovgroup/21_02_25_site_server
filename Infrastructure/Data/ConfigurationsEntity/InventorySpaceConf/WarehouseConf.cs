using Domain.Entities.InventorySpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.InventorySpaceConf
{
    /// <summary>
    /// Настройка сущности Warehouse.
    /// Определяет связи с адресами и товарными запасами.
    /// </summary>
    public class WarehouseConf : AuditableEntityConf<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> entity)
        {
            base.Configure(entity);

           
            // Связь с адресом доставки
            entity.HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<Warehouse>(e => e.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
 

            // Связь с товарными запасами
            entity.HasMany(e => e.ProductStocks)
                .WithOne(ps => ps.Warehouse)
                .HasForeignKey(ps => ps.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
