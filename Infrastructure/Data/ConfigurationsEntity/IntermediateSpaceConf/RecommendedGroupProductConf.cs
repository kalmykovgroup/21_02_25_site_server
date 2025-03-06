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
    public class RecommendedGroupProductConf : IEntityTypeConfiguration<RecommendedGroupProduct>
    {
        public void Configure(EntityTypeBuilder<RecommendedGroupProduct> builder)
        {
            builder.ToTable("recommended_group_products");

            // Составной ключ
            builder.HasKey(e => new { e.ProductId, e.RecommendedGroupId });

            builder.Property(e => e.ProductId).HasColumnName("product_id");
            builder.Property(e => e.RecommendedGroupId).HasColumnName("recommended_group_id");

            // Связь с продуктом
            builder.HasOne(e => e.Product)
                .WithMany(p => p.RecommendedGroupProducts)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с тегом
            builder.HasOne(e => e.RecommendedGroup)
                .WithMany(rg => rg.RecommendedGroupProducts)
                .HasForeignKey(e => e.RecommendedGroupId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
