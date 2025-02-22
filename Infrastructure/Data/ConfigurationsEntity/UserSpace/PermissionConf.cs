using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserSpace;
using Infrastructure.Data.ConfigurationsEntity.Common; 

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace
{ 

    public class PermissionConf : AuditableEntityConf<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            base.Configure(entity);

            entity.ToTable("permissions"); 
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");
          
        }
         

    }
}
