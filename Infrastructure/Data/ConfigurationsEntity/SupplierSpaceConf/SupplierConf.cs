 
using Domain.Entities.SupplierSpace; 
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.SupplierSpaceConf
{
    public class SupplierConf : AuditableEntityConf<Supplier>
    {

        /// <summary>
        /// Настройка сущности Supplier.
        /// Определяет связи с адресами, продуктами и брендами.
        /// </summary>
        public override void Configure(EntityTypeBuilder<Supplier> entity)
        {
            base.Configure(entity);


            entity.ToTable("suppliers");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(t => t.Name).IsRequired().HasColumnName("name").HasMaxLength(255);
                entity.Property(t => t.Description).IsRequired(false).HasColumnName("description").HasMaxLength(1000);

                entity.Property(e => e.PhoneNumber).IsRequired(false).HasColumnName("phone_number")
                .HasMaxLength(20);

                entity.Property(e => e.Email).IsRequired(false).HasColumnName("email")
                   .HasMaxLength(255);

                entity.Property(e => e.IsActive).IsRequired().HasColumnName("is_active");
                   
             
        }
    }
    
}
