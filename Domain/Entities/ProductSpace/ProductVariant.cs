using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Сущность, представляющая вариацию продукта.
    /// Используется для управления характеристиками продуктов, такими как цвет или размер.
    /// </summary> 
    public class ProductVariant : SeoEntity<ProductVariant>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор продукта, связанного с вариацией.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт, для которого определена вариация.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Название вариации (например, "Цвет").
        /// </summary> 
        public string VariantName { get; set; } = string.Empty;

        /// <summary>
        /// Значение вариации (например, "Красный").
        /// </summary> 
        public string VariantValue {  get; set; } = string.Empty;

        /// <summary>
        /// Количество товара, доступное для этой вариации.
        /// </summary> 
        public int StockQuantity { get; set; }

      
    }

}
