using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AnalyticsSpace;
using Domain.Entities.UserSpace.UserTypes;
using Infrastructure.Data.ConfigurationsEntity.Common;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace.UserTypes
{
    public class CustomerConf : AuditableEntityConf<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> entity)
        {
            base.Configure(entity);

            entity.ToTable("customers");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");


                entity.Property(e => e.CustomerGroupId).IsRequired().HasColumnName("customer_group_id"); 
                entity.Property(e => e.CustomerPreferenceId).IsRequired().HasColumnName("customer_preference_id"); 



                // Связь с группой клиентов
                entity.HasOne(c => c.CustomerGroup)
                    .WithMany(cg => cg.Customers)
                    .HasForeignKey(c => c.CustomerGroupId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Настройка связи с CustomerPreference (один-к-одному) Связь с предпочтениями клиента
                entity.HasOne(c => c.CustomerPreference)
                    .WithOne() // Навигационное свойство в CustomerPreference
                    .HasForeignKey<Customer>(cp => cp.CustomerPreferenceId) // Внешний ключ в CustomerPreference
                    .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

                entity
                    .HasOne(c => c.User)  // Связь 1 к 1 с UserEntity
                    .WithOne(u => u.Customer)
                    .HasForeignKey<Customer>(c => c.Id) // FK = UserEntity.Id
                    .OnDelete(DeleteBehavior.Cascade);
 
        }



    }
}
