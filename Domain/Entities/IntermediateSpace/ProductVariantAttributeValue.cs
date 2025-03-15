using Domain.Entities.CategorySpace;
using Domain.Entities.ProductSpace;

namespace Domain.Entities.IntermediateSpace;

/// <summary>
/// Связка "Вариант товара" + "Атрибут" + "Значение атрибута".
/// Например, Variant "iPhone 14, Black, 128GB" => (Color=Black), (Memory=128GB).
/// </summary>
public class ProductVariantAttributeValue
{
     
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на конкретный вариант (SKU).
    /// </summary> 
    public Guid ProductVariantId { get; set; }

    /// <summary>
    /// Ссылка на атрибут (CategoryAttribute).
    /// Хранится, чтобы упростить валидацию и индексацию.
    /// </summary> 
    public Guid CategoryAttributeId { get; set; }

    /// <summary>
    /// Ссылка на конкретное значение (например, "Black", "128GB").
    /// </summary> 
    public Guid CategoryAttributeValueId { get; set; }

    // Навигационные свойства. 
    public virtual ProductVariant ProductVariant { get; set; } = null!;
 
    public virtual CategoryAttribute CategoryAttribute { get; set; } = null!;
 
    public virtual CategoryAttributeValue CategoryAttributeValue { get; set; } = null!;
}