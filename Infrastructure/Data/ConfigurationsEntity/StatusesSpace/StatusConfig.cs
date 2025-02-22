using Domain.Entities.ProductSpace;
using Domain.Entities.StatusesSpace;
using Domain.Entities.StatusesSpace.Heirs; 
using Infrastructure.Data.ConfigurationsEntity.Common; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.StatusesSpace
{
    public class StatusConfig : AuditableEntityConf<Status>
    {
        public override void Configure(EntityTypeBuilder<Status> entity)
        {
            base.Configure(entity);

            // TPH-структура
            entity.HasDiscriminator<StatusType>(d => d.StatusType)
                    .HasValue<OrderStatus>(StatusType.OrderStatus) 
                    .HasValue<ShippingStatus>(StatusType.ShippingStatus)
                    .HasValue<Status>(StatusType.Unknown);

            entity.ToTable("statuses");
            entity.HasKey(x => x.Id);
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.StatusType).HasColumnName("status_type");

            entity.Property(e => e.IsDefault).HasColumnName("is_default");

            entity.Property(e => e.SortOrder).HasColumnName("sort_order");

 
        }
         
    }
}
