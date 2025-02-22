
using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.AddressesSpace
{
    public class AddressConfig : AuditableEntityConf<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);


            // Настройка TPH для дискриминатора FileCategory
            builder.HasDiscriminator<AddressType>(t => t.AddressType)
                .HasValue<Address>(AddressType.General)
                .HasValue<SupplierAddress>(AddressType.Supplier)
                .HasValue<UserAddress>(AddressType.User);

            builder.ToTable("addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AddressType)
                .HasColumnName("entity_type")
                .IsRequired();

            builder.Property(a => a.Label)
                .HasColumnName("label")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.State)
                .HasColumnName("state")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Country)
                .HasColumnName("country")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.AdditionalInfo)
                .HasColumnName("additional_info")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(a => a.IsPrimary)
                .HasColumnName("is_primary")
                .IsRequired();

        }
        
      
    }
}
