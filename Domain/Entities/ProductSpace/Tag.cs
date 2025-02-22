using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.IntermediateSpace;

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Сущность, представляющая тег.
    /// Используется для категоризации, фильтрации и SEO-оптимизации продуктов.
    /// </summary> 
    public class Tag : AuditableEntity<Tag>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Название тега.
        /// Например, "Новинка", "Скидка" или "Популярное".
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Продукты, связанные с данным тегом.
        /// Например, товары, участвующие в распродаже или акции.
        /// </summary>
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        
    }

}
