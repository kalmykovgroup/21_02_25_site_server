using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ProductSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace
{
    public class ProductConf : SeoEntityConf<Product>
    {

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("products");
                builder.HasKey(x => x.Id);

                builder.Property(p => p.Id).HasColumnName("id").IsRequired();
                builder.Property(p => p.CategoryId).HasColumnName("category_id").IsRequired();
                builder.Property(p => p.SupplierId).HasColumnName("supplier_id").IsRequired();

                builder.Property(p => p.Name).HasColumnName("name").IsRequired();
                builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(1000).IsRequired(false);

            builder.Property(e => e.Url)
                .HasColumnName("url")         // Название колонки в БД
                .HasColumnType("varchar(2048)") // Тип данных в БД (PostgreSQL / MySQL)
                .HasMaxLength(2048)           // Максимальная длина строки
                .IsRequired();                // Обязательное поле


            builder.Property(p => p.OriginalPrice).HasColumnName("original_price").IsRequired(false).HasPrecision(18, 2);

                builder.Property(p => p.DiscountPercentage).HasColumnName("discount_percentage").IsRequired(false).HasPrecision(5, 2);

                builder.Property(p => p.Price).HasColumnName("price").IsRequired().HasPrecision(18, 2);

                builder.Property(p => p.Rating).HasColumnName("rating").HasPrecision(3, 2).HasDefaultValue(5); 
                builder.Property(p => p.NumberOfReviews).HasColumnName("number_of_reviews").HasDefaultValue(0);  


                //  builder.Property(p => p.DiscountPercentage).HasColumnName("discount_percentage").IsRequired(false).HasPrecision(5, 2);
                builder.Property(p => p.IsActive).HasColumnName("is_active").IsRequired();
                builder.Property(p => p.BrandId).HasColumnName("brand_id").IsRequired();
                // Связь с категорией
                  builder.HasOne(e => e.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Связь с поставщиком
                builder.HasOne(e => e.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь с брендом
                 builder.HasOne(e => e.Brand)
                     .WithMany(b => b.Products)
                     .HasForeignKey(e => e.BrandId)
                     .OnDelete(DeleteBehavior.Cascade);
 
        }
    }

}
