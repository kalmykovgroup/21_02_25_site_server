using Domain.Entities._Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.StorageSpace.Heirs
{
    /// <summary>
    /// Настройка сущности ProductFile.
    /// Определяет связи с продуктами.
    /// </summary>
    public class ProductFileConf : IEntityTypeConfiguration<ProductFile>
    {
        public void Configure(EntityTypeBuilder<ProductFile> builder)
        {
            // Указываем, что ProductFile наследует FileStorage
            builder.HasBaseType<FileStorage>();

            builder.Property(t => t.ProductId).HasColumnName("product_id");

            // Связь с продуктом
            builder.HasOne(e => e.Product)
                .WithMany(p => p.ProductFiles)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
