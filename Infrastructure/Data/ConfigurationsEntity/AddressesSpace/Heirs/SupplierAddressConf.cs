using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.AddressesSpace.Heirs
{
    public class SupplierAddressConf : IEntityTypeConfiguration<SupplierAddress>
    { 

        public void Configure(EntityTypeBuilder<SupplierAddress> builder)
        {
            // Указываем, что ProductFile наследует FileStorage
            builder.HasBaseType<Address>();

            builder.Property(t => t.SupplierId).HasColumnName("supplier_id");

            // Связь с продуктом
            builder.HasOne(e => e.Supplier)
                .WithMany(s => s.Addresses)
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
