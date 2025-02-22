using Domain.Entities.IntermediateSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.IntermediateSpaceConf
{
    public class SupplierBrandConf : IEntityTypeConfiguration<SupplierBrand>
    {
        /// <summary>
        /// Настройка сущности SupplierBrand
        /// </summary>
        public void Configure(EntityTypeBuilder<SupplierBrand> builder)
        {
            builder.ToTable("supplier_brands");

            // Настройка составного ключа
            builder.HasKey(sb => new { sb.SupplierId, sb.BrandId });

            // Связь с поставщиком
            builder.HasOne(sb => sb.Supplier)
                .WithMany(s => s.SupplierBrands)
                .HasForeignKey(sb => sb.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с брендом
            builder.HasOne(sb => sb.Brand)
                .WithMany(b => b.SupplierBrands)
                .HasForeignKey(sb => sb.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
