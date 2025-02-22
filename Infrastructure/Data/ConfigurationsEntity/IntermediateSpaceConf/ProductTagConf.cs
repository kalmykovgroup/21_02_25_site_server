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
    public class ProductTagConf : IEntityTypeConfiguration<ProductTag>
    {
        /// <summary>
        /// Настройка сущности ProductTag.
        /// Определяет составной ключ и связи с продуктами и тегами.
        /// </summary>
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("product_tags");

            // Составной ключ
            builder.HasKey(e => new { e.ProductId, e.TagId });

            // Связь с продуктом
            builder.HasOne(e => e.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с тегом
            builder.HasOne(e => e.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
