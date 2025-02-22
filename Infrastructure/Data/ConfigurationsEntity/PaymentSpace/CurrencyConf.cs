using Domain.Entities.Common;
using Domain.Entities.OrderSpace;
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
    public class CurrencyConf : AuditableEntityConf<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> entity)
        {
            base.Configure(entity);
            entity.ToTable("currencies"); // Название таблицы

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(x => x.Code)
                    .HasColumnName("code")
                    .HasMaxLength(3) // ISO 4217 (USD, EUR и т. д.)
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsRequired();
          
        }
    }
}
