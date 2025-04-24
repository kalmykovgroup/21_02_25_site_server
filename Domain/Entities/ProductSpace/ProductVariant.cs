using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;

namespace Domain.Entities.ProductSpace;

/// <summary>
/// Конкретный вариант (SKU) товара.
/// Например, "iPhone 14, Black, 128GB".
/// </summary>
public class ProductVariant : AuditableEntity<ProductVariant>
{
    
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на товар (Product).
    /// В задаче класс Product не приводим, поэтому только Id.
    /// </summary> 
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    /// <summary>
    /// Уникальный артикул (SKU), обязательный.
    /// </summary> 
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Допустим, внутренний или международный штрихкод.
    /// Может быть не у каждого товара, поэтому null.
    /// </summary> 
    public string Barcode { get; set; } = string.Empty;

    /// <summary>
    /// Флаг "Активен ли вариант", чтобы временно отключать SKU.
    /// </summary>
    public bool IsActive { get; set; } = true;
 

    // Навигационное свойство: один SKU может иметь много пар "атрибут-значение".
    public virtual ICollection<ProductVariantAttributeValue> ProductVariantAttributeValues { get; set; } = new List<ProductVariantAttributeValue>();
    public virtual ICollection<SellerOffer> SellerOffers { get; set; } = new List<SellerOffer>();
}