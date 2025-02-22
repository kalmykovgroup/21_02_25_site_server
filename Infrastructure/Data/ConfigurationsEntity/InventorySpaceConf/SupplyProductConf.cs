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
    public class SupplyProductConf : AuditableEntityConf<SupplyProduct>
    {
        public override void Configure(EntityTypeBuilder<SupplyProduct> entity)
        {
            base.Configure(entity);

            
            entity.ToTable("supply_products");
            entity.HasKey(sp => sp.Id);
            entity.Property(sp => sp.Quantity).IsRequired();
            entity.Property(sp => sp.PurchasePrice).HasColumnType("decimal(18,2)");
            entity.Property(sp => sp.ReceivedDate).IsRequired();
            entity.HasOne(sp => sp.Supply)
                .WithMany(s => s.SupplyProducts)
                .HasForeignKey(sp => sp.SupplyId);
            entity.HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
            
        }
    }
}
