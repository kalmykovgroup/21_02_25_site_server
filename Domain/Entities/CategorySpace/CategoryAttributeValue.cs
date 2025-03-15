using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;

namespace Domain.Entities.CategorySpace;

/// <summary>
/// Возможное значение характеристики.
/// Например, "128 ГБ", "256 ГБ" для атрибута "Объём памяти".
/// </summary>
public class CategoryAttributeValue : AuditableEntity<CategoryAttributeValue>
{
   
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ссылка на атрибут (CategoryAttribute).
    /// </summary> 
    public Guid CategoryAttributeId { get; set; }

    /// <summary>
    /// Само значение (например, "Black", "128GB").
    /// </summary> 
    public string Value { get; set; } = string.Empty;
    
    // Навигационное свойство для связи "многие к одному".
    // Сам класс CategoryAttribute приведён выше. 
    public virtual CategoryAttribute CategoryAttribute { get; set; } = null!;
}