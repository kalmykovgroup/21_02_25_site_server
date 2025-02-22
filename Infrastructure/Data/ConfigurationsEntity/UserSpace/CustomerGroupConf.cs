using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserSpace; 
using Domain.Entities.UserSpace.UserTypes;
using Infrastructure.Data.ConfigurationsEntity.Common;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace
{
    public class CustomerGroupConf : AuditableEntityConf<CustomerGroup>
    {
        public override void Configure(EntityTypeBuilder<CustomerGroup> entity)
        {
            base.Configure(entity);


            entity.ToTable("customer_group");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                // Настройка связи с клиентами
                entity.HasMany(e => e.Customers)
                    .WithOne(c => c.CustomerGroup)
                    .HasForeignKey(c => c.CustomerGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
  
        }
         
        
    }
}
