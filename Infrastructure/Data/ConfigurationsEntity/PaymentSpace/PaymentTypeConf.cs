using Domain.Entities.Common;
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
    public class PaymentTypeConf : AuditableEntityConf<PaymentType>
    {
        public override void Configure(EntityTypeBuilder<PaymentType> entity)
        {
            base.Configure(entity);

            entity.ToTable("payment_types");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(255);
          
        }
    }
}
