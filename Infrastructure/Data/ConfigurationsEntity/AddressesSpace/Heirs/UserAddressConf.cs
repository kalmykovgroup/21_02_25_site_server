using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.AddressesSpace.Heirs
{
    public class UserAddressConf : IEntityTypeConfiguration<UserAddress>
    { 
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            // Указываем, что ProductFile наследует FileStorage
            builder.HasBaseType<Address>();


            builder.HasOne(e => e.User)
                .WithMany(s => s.Addresses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
