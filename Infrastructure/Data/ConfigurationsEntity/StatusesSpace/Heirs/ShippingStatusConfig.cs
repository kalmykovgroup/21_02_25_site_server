using Domain.Entities.StatusesSpace;
using Domain.Entities.StatusesSpace.Heirs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.StatusesSpace.Heirs
{
    public class ShippingStatusConfig : IEntityTypeConfiguration<ShippingStatus>
    {
        public void Configure(EntityTypeBuilder<ShippingStatus> builder)
        {
            builder.HasBaseType<Status>();
        }
    }
}
