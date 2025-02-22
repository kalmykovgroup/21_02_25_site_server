 
using Domain.Entities.ProductSpace; 
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace
{
    public class ProductAttributeConf : SeoEntityConf<ProductAttribute>
    {
        public override void Configure(EntityTypeBuilder<ProductAttribute> entity)
        {
            base.Configure(entity);

            // Связь с продуктом
            entity.HasOne(e => e.Product)
                    .WithMany(p => p.Attributes)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
