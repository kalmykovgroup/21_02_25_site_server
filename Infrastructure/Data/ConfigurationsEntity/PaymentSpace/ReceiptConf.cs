using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.PaymentSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.PaymentSpace
{
    /// <summary>
    /// Сущность, представляющая чек, выданный по заказу.
    /// Хранит информацию о сумме оплаты, валюте, методе оплаты,
    /// а также ссылку на заказ, для которого был создан чек.
    /// </summary> 
    public class ReceiptConf : AuditableEntityConf<Receipt>
    {


        /// <summary>
        /// Настройка сущности Receipt.
        /// Определяет связи и дополнительные ограничения.
        /// </summary>
        public override void Configure(EntityTypeBuilder<Receipt> entity)
        {
            base.Configure(entity);

            entity.ToTable("receipts");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");

                entity.Property(x => x.OrderId).HasColumnName("order_id");

                entity.Property(x => x.IssuedAt)
                    .HasColumnName("issued_at")
                     .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                entity.Property(x => x.AmountPaid)
                    .HasColumnName("amount_paid")
                    .HasPrecision(18, 2)
                    .IsRequired();

                entity.Property(x => x.CurrencyId).HasColumnName("currency_id");

                entity.Property(x => x.PaymentTypeId).HasColumnName("payment_type_id");

                entity.Property(x => x.TransactionId).HasColumnName("transaction_id");

                entity.Property(x => x.ReceiptUrl)
                    .HasColumnName("receipt_url")
                    .HasMaxLength(500);

                entity.Property(x => x.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(1000);
            


        }
    }

}
