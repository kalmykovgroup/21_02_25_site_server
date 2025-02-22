using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserSpace;
using Domain.Entities.AddressesSpace;
using Domain.Entities.Common;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Entities.AnalyticsSpace;

namespace Infrastructure.Data.ConfigurationsEntity.UserSpace
{
    //TPH (Общая таблица для Employee, UserCustomer)
    public class UserConf : AuditableEntityConf<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            base.Configure(entity);

            entity.ToTable("users");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number")
                   .HasMaxLength(20);

           
                // Уникальный составной ключ (PhoneNumber, UserType)
                entity.HasIndex(u => new { u.PhoneNumber, u.UserType })
                    .IsUnique()
                    .HasDatabaseName("IX_User_PhoneNumber_UserType");

                // Уникальный составной ключ (PhoneNumber, UserType)
                entity.HasIndex(u => new { u.Email, u.UserType })
                    .IsUnique()
                    .HasDatabaseName("IX_User_Email_UserType");


                entity.Property(e => e.FirstName).HasColumnName("first_name")
                 .IsRequired(false)
                 .HasMaxLength(50);

                entity.Property(e => e.LastName).HasColumnName("last_name")
                    .IsRequired(false)
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasColumnName("patronymic")
                    .IsRequired(false)
                    .HasMaxLength(100);

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth")
                    .HasColumnType("timestamp")
                   .IsRequired(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired(false);


                entity.Property(u => u.WishListId).HasColumnName("wish_list_id");

                entity.HasOne(u => u.WishList)
                 .WithOne(wl => wl.User)
                 .HasForeignKey<User>(u => u.WishListId)
                 .OnDelete(DeleteBehavior.Cascade); // Если нужно каскадное удаление

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired(false);


                entity.Property(e => e.IsActive).HasColumnName("is_active")
                    .IsRequired();

                entity.Property(e => e.AddressType).HasColumnName("address_type")
                    .IsRequired(); 
                 
                // Связь с CreatedByUser
                entity.HasOne(u => u.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(u => u.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Связь с UpdatedByUser
                entity.HasOne(u => u.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(u => u.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Связь с DeletedByUser
                entity.HasOne(u => u.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(u => u.DeletedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
 
        }

    }
}
