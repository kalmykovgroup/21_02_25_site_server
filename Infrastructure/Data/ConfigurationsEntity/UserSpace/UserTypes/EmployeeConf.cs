using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserSpace.UserTypes;
using Infrastructure.Data.ConfigurationsEntity.Common;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace.UserTypes
{

    public class EmployeeConf : AuditableEntityConf<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> entity)
        {
            base.Configure(entity);

            entity.ToTable("employees");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");


                // 🔹 Настройка поля Position
                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("position");

                // 🔹 Настройка поля HiredAt
                entity.Property(e => e.HiredAt)
                    .IsRequired()
                    .HasColumnName("hired_at");

                // 🔹 Настройка поля TerminatedAt (может быть NULL)
                entity.Property(e => e.TerminatedAt).IsRequired(false)
                    .HasColumnName("terminated_at");

                // 🔹 Настройка поля Notes (необязательное, макс. 2000 символов)
                entity.Property(e => e.Notes).IsRequired(false)
                    .HasMaxLength(2000)
                    .HasColumnName("notes");


                // Связь один-к-одному с UserEntity
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Employee)
                    .HasForeignKey<Employee>(e => e.Id) // FK = UserEntity.Id
                    .OnDelete(DeleteBehavior.Cascade);
 
        }

    }
}
