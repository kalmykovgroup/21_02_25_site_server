using Domain.Entities.PaymentSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.PaymentSpace
{
    public class PaymentCardConf : AuditableEntityConf<PaymentCard>
    {
        public override void Configure(EntityTypeBuilder<PaymentCard> entity)
        {
            base.Configure(entity);
            entity.ToTable("payment_cards");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");

                entity.Property(x => x.UserId).HasColumnName("user_id");

                entity.Property(x => x.CardType)
                    .HasColumnName("card_type")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.Last4Digits)
                    .HasColumnName("last4_digits")
                    .HasMaxLength(4)
                    .IsRequired();

                entity.Property(x => x.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .IsRequired();

                entity.Property(x => x.Token)
                    .HasColumnName("token")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.MaskedCardNumber)
                    .HasColumnName("masked_card_number")
                    .HasMaxLength(20)
                    .IsRequired();
          
        }
    }
}
