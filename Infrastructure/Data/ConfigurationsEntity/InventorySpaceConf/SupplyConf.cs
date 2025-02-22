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
    public class SupplyConf : AuditableEntityConf<Supply>
    {

        public override void Configure(EntityTypeBuilder<Supply> entity)
        {
            base.Configure(entity);

           
            entity.ToTable("supplies");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SupplyDate).IsRequired();
            entity.Property(s => s.InvoiceNumber).HasMaxLength(50);
            entity.Property(s => s.Status).HasMaxLength(20);
            entity.HasOne(s => s.Supplier)
                .WithMany()
                .HasForeignKey(s => s.SupplierId);
           
        }
    }
}
