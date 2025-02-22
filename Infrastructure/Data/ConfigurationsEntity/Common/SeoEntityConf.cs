using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.Common
{
    public class SeoEntityConf<TEntity> : AuditableEntityConf<TEntity> where TEntity : SeoEntity<TEntity>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            /// <summary>
            /// Мета-заголовок для SEO.
            /// </summary> 
            builder.Property(e => e.SeoTitle).HasColumnName("seo_title").HasMaxLength(150);

            /// <summary>
            /// Мета-описание для SEO.
            /// </summary>  
            builder.Property(e => e.SeoDescription).HasColumnName("seo_description").HasMaxLength(300);

            /// <summary>
            /// Ключевые слова для SEO.
            /// </summary>  
            builder.Property(e => e.SeoKeywords).HasColumnName("seo_keywords").HasMaxLength(200);
        }
    }
}
