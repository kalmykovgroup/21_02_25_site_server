using Domain.Entities.UserSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace
{
    public class PhoneVerificationCodeConf : IEntityTypeConfiguration<PhoneVerificationCode>
    {
      
        public void Configure(EntityTypeBuilder<PhoneVerificationCode> builder)
        {
            builder.ToTable("phone_verification_codes");

            builder.HasKey(pvc => pvc.Id);

            builder.Property(pvc => pvc.Id)
                .HasColumnName("id");

            builder.Property(pvc => pvc.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("phone_number");

            builder.Property(pvc => pvc.Code)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("code");

            builder.Property(pvc => pvc.CountSendMessage)
                .IsRequired()
                .HasColumnName("count_send_message");

            builder.Property(pvc => pvc.NumberOfAttempts)
                .IsRequired()
                .HasColumnName("number_of_attempts");

            builder.Property(pvc => pvc.AllCountSendMessage)
                .IsRequired()
                .HasColumnName("all_count_send_message");

            builder.Property(pvc => pvc.NumberOfMessagesSentBeforeLoggingIn)
                .IsRequired()
                .HasColumnName("number_of_messages_sent_before_logging_in");

            builder.Property(pvc => pvc.CodeLifetimeSeconds)
                .IsRequired()
                .HasColumnName("code_lifetime_seconds");


            builder.Property(pvc => pvc.UnblockingTimeSeconds)
                .IsRequired()
                .HasColumnName("unblocking_time_seconds");

            // Уникальный индекс для предотвращения дублирования кодов
            builder.HasIndex(pvc => new { pvc.PhoneNumber, pvc.Code })
                .IsUnique()
                .HasDatabaseName("ix_phone_verification_code_phone_number_code");
        }
    }
}
