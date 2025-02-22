
using Domain.Entities.ProductSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Сущность, представляющая связь между продуктом и тегом.
    /// Используется для категоризации и фильтрации продуктов с помощью тегов.
    /// </summary> 
    public class ProductTag
    {
        /// <summary>
        /// Уникальный идентификатор продукта, связанного с тегом.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор тега, связанного с продуктом.
        /// </summary> 
        public Guid TagId { get; set; }

        /// <summary>
        /// Ссылка на тег.
        /// </summary>
        public virtual Tag Tag { get; set; } = null!;

       
    }

}
