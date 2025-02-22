
using Domain.Entities.Common;
using Domain.Entities.PaymentSpace;
using Domain.Entities.StatusesSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.ConfigurationsEntity.PaymentSpace
{


    /// <summary>
    /// Сущность, представляющая детали платежа.
    /// Хранит информацию о способе оплаты, статусе платежа,
    /// дате оплаты и связанных заказах.
    /// </summary> 
    public class PaymentDetailsConf : AuditableEntityConf<PaymentDetails>
    {

        /// <summary>
        /// Настройка сущности PaymentDetails.
        /// Определяет связи с заказами, методами оплаты и статусами.
        /// </summary>
        public override void Configure(EntityTypeBuilder<PaymentDetails> entity)
        {
            base.Configure(entity);

            entity.ToTable("payment_details");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");

                entity.Property(x => x.OrderId).HasColumnName("order_id");

                entity.Property(x => x.PaymentMethodId).HasColumnName("payment_method_id");

                entity.Property(x => x.CurrencyId).HasColumnName("currency_id");

                entity.Property(x => x.ExchangeRate)
                    .HasColumnName("exchange_rate")
                    .HasColumnType("decimal(18,6)")
                    .IsRequired();

                entity.Property(x => x.PaymentStatusId).HasColumnName("payment_status_id");

                entity.Property(x => x.PaymentCardId).HasColumnName("payment_card_id");

                // ✅ Явно указываем внешний ключ
                entity.HasOne(x => x.PaymentMethod)
                    .WithMany()
                    .HasForeignKey(x => x.PaymentMethodId)
                    .OnDelete(DeleteBehavior.SetNull); 

                // ✅ Явно указываем внешний ключ
                entity.HasOne(x => x.PaymentCard)
                    .WithMany()
                    .HasForeignKey(x => x.PaymentCardId)
                    .OnDelete(DeleteBehavior.SetNull); 

                        entity.Property(x => x.TotalPaid)
                    .HasColumnName("total_paid")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(x => x.TotalRefunded)
                    .HasColumnName("total_refunded")
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(x => x.AuthorizedAmount)
                    .HasColumnName("authorized_amount")
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(x => x.IsAuthorized)
                    .HasColumnName("is_authorized")
                    .HasDefaultValue(false);

                entity.Property(x => x.PaidAt)
                    .HasColumnName("paid_at");

                entity.HasMany(x => x.PaymentTransactions)
                    .WithOne(x => x.PaymentDetails)
                    .HasForeignKey(x => x.PaymentDetailsId)
                    .OnDelete(DeleteBehavior.Cascade);

            
        }
    }

}
