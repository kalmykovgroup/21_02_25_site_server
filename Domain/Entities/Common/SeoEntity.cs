using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class SeoEntity<TEntity> : AuditableEntity<TEntity> where TEntity : class
    {
        /// <summary>
        /// SEO-заголовок сущности, получаемый на основе текущего языка.
        /// </summary> 
        public string SeoTitle {  get; set; } = string.Empty;

        /// <summary>
        /// SEO-описание сущности, получаемое на основе текущего языка.
        /// </summary> 
        public string SeoDescription { get; set; } = string.Empty;

        /// <summary>
        /// SEO-ключевые слова сущности, получаемые на основе текущего языка.
        /// </summary> 
        public string SeoKeywords { get; set; } = string.Empty;
    }
}
