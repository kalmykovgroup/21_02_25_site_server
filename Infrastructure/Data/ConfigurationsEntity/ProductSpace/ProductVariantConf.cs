using Domain.Entities.ProductSpace; 
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace
{
    public class ProductVariantConf : SeoEntityConf<ProductVariant>
    {
        public override void Configure(EntityTypeBuilder<ProductVariant> entity)
        {
            base.Configure(entity);

            // Связь с продуктом
            entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
