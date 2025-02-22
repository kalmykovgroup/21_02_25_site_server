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
    public class ProductDiscountConf : IEntityTypeConfiguration<ProductDiscount>
    {
         
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.ToTable("product_discounts");

            builder.HasKey(pd => new { pd.ProductId, pd.DiscountRuleId });

            builder.Property(pd => pd.ProductId).HasColumnName("product_id");
            builder.Property(pd => pd.DiscountRuleId).HasColumnName("discount_rule_id");

            builder.HasOne(pd => pd.Product)
                .WithMany()
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pd => pd.DiscountRule)
                .WithMany()
                .HasForeignKey(pd => pd.DiscountRuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
