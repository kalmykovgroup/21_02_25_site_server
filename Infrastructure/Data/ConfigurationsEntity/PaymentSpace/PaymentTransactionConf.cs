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
    public class PaymentTransactionConf : AuditableEntityConf<PaymentTransaction>
    {
        public override void Configure(EntityTypeBuilder<PaymentTransaction> entity)
        {
            base.Configure(entity);

            entity.ToTable("payment_transactions");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");

                entity.Property(x => x.PaymentDetailsId).HasColumnName("payment_details_id");

                entity.Property(x => x.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(x => x.TransactionDate).HasColumnName("transaction_date");

                entity.Property(x => x.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.PaymentStatusId).HasColumnName("payment_status_id");

                entity.Property(x => x.TransactionType).HasColumnName("transaction_type");

                entity.Property(x => x.ParentTransactionId).HasColumnName("parent_transaction_id");

                entity.Property(x => x.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.HasOne(x => x.ParentTransaction)
                    .WithMany()
                    .HasForeignKey(x => x.ParentTransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
          
        }
    }
}
