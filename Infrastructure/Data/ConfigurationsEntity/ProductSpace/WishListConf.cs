using Domain.Entities.UserSpace;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace
{
    /// <summary>
    /// Список желаний клиента
    /// </summary> 
    public class WishListConf : IEntityTypeConfiguration<WishList>
    { 

        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("wish_lists");

            // Настройка ключа
            builder.HasKey(wl => wl.Id);
        }
    }

}
