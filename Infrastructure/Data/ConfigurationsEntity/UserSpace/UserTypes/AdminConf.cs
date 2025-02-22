using Domain.Entities.Common;
using Domain.Entities.SupplierSpace;
using Domain.Entities.UserSpace.UserTypes;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace.UserTypes
{
    public class AdminConf : AuditableEntityConf<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> entity)
        {
            base.Configure(entity);

            entity.ToTable("admins");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity
                    .HasOne(c => c.User)  // Связь 1 к 1 с UserEntity
                    .WithOne(u => u.Admin)
                    .HasForeignKey<Admin>(c => c.Id) // FK = UserEntity.Id
                    .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
