 
using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Сущность, представляющая характеристику продукта.
    /// Хранит название и значение характеристики, а также информацию о её важности.
    /// </summary> 
    public class ProductAttribute : SeoEntity<ProductAttribute>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор продукта, к которому относится характеристика.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Название характеристики (например, "Цвет").
        /// </summary>  
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Значение характеристики (например, "Красный").
        /// </summary> 
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Признак важной характеристики (например, для отображения в кратком описании).
        /// </summary> 
        public bool IsImportant { get; set; } = false;

    }


}
