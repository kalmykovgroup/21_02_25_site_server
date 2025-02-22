using Domain.Entities.BrandSpace;
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
    /// Настройка сущности ProductStock.
    /// Определяет связи с товарами и складами.
    /// </summary>
    public class ProductStockConf : AuditableEntityConf<ProductStock>
    {

        public override void Configure(EntityTypeBuilder<ProductStock> entity)
        {
            base.Configure(entity);

           
            // Связь с товаром
            entity.HasOne(e => e.Product)
                .WithMany(p => p.ProductStocks)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь со складом
            entity.HasOne(e => e.Warehouse)
                .WithMany(w => w.ProductStocks)
                .HasForeignKey(e => e.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
