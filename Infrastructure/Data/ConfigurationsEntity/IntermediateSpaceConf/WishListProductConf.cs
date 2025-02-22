using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.IntermediateSpaceConf
{
    public class WishListProductConf : IEntityTypeConfiguration<WishListProduct>
    { 
        public void Configure(EntityTypeBuilder<WishListProduct> builder)
        {
            // Указываем имя таблицы
            builder.ToTable("wish_list_products");

            // Устанавливаем составной первичный ключ
            builder.HasKey(wp => new { wp.WishListId, wp.ProductId });

            // Настраиваем внешний ключ для WishList
            builder.HasOne(wp => wp.WishList)
                   .WithMany(w => w.WishListProducts)
                   .HasForeignKey(wp => wp.WishListId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Настраиваем внешний ключ для ProductEntity
            builder.HasOne(wp => wp.Product)
                   .WithMany(p => p.WishListProducts)
                   .HasForeignKey(wp => wp.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
