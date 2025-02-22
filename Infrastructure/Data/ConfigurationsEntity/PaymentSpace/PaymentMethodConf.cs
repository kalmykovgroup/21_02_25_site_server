
using Domain.Entities.UserSpace;
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.PaymentSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.PaymentSpace
{
    /// <summary>
    /// Сущность, представляющая метод оплаты.
    /// Хранит информацию о типе оплаты, связанных клиентах,
    /// а также дополнительных данных, таких как номер карты или её срок действия.
    /// </summary> 
    public class PaymentMethodConf : AuditableEntityConf<PaymentMethod>
    {


        /// <summary>
        /// Настройка сущности PaymentMethod.
        /// Включает определение связей и дополнительных ограничений.
        /// </summary>
        public override void Configure(EntityTypeBuilder<PaymentMethod> entity)
        {
            base.Configure(entity);

            entity.ToTable("payment_methods");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");

                entity.Property(x => x.CustomerId).HasColumnName("customer_id");

                entity.Property(x => x.PaymentTypeId).HasColumnName("payment_type_id");

                entity.Property(x => x.RequiresAdditionalDetails)
                    .HasColumnName("requires_additional_details")
                    .HasDefaultValue(false);

                entity.Property(x => x.IsPrimary)
                    .HasColumnName("is_primary")
                    .HasDefaultValue(false);
            
        }
    }

}
