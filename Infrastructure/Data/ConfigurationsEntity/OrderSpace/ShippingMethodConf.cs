using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.OrderSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.OrderSpace
{
    /// <summary>
    /// Сущность, представляющая различные способы доставки.
    /// Например, "Курьер", "Самовывоз", "Почта".
    /// Хранит информацию о названии метода, стоимости и его описании.
    /// </summary> 
    public class ShippingMethodConf : AuditableEntityConf<ShippingMethod>
    {

        /// <summary>
        /// Настройка сущности ShippingMethod.
        /// Определяет дополнительные ограничения.
        /// </summary>
        public override void Configure(EntityTypeBuilder<ShippingMethod> entity)
        {
            base.Configure(entity);
            entity.ToTable("shipping_methods");

                entity.HasKey(dr => dr.Id);
                entity.Property(e => e.Id).HasColumnName("id");

    
                entity.Property(e => e.Cost).IsRequired().HasColumnName("cost").HasPrecision(18, 2); // 18 знаков в целом, 2 после запятой;
                entity.Property(e => e.IsActive).IsRequired().HasColumnName("is_active");
           
        }
    }

}
