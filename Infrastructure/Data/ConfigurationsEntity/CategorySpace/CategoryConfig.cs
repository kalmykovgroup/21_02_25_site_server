﻿ 
using Domain.Entities.CategorySpace; 
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.CategorySpace
{
    public class CategoryConfig : SeoEntityConf<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> entity)
        {
            base.Configure(entity);
 
            entity.ToTable("categories");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id).HasColumnName("id");

            entity.Property(c => c.ParentCategoryId).IsRequired(false).HasColumnName("parent_category_id");

            entity.Property(c => c.IsActive).HasColumnName("is_active").HasDefaultValue(true);

            entity.Property(c => c.Icon).HasColumnName("icon").HasMaxLength(2048);

            entity.Property(c => c.Level).HasColumnName("level").IsRequired();
            entity.Property(c => c.Index).HasColumnName("index").IsRequired();
 

            entity.Property(c => c.Path).HasColumnName("path").HasMaxLength(100);

            // Настройка связи с родительской категорией
            entity.HasOne(e => e.ParentCategory)
                .WithMany(e => e.SubCategories)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                  
                  

           
        }   
    }
}
