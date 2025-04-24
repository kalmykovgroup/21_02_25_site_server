using Domain.Entities.Common;

namespace Domain.Entities.ProductSpace;

 
/// <summary>
/// Предложение продавца на конкретный вариант товара (SKU).
/// Например, "Продавец A" продает "iPhone 14 Black 128GB" за 1000 у.е.
/// </summary>
public class SellerOffer: AuditableEntity<SellerOffer>
{
    
    public Guid Id { get; set; }

    /// <summary>
    /// Ссылка на variant (SKU).
    /// </summary> 
    public Guid ProductVariantId { get; set; }

    /// <summary>
    /// Ссылка на продавца.
    /// </summary> 
    public Guid SellerId { get; set; }

    /// <summary>
    /// Цена за единицу товара.
    /// </summary> 
    public decimal Price { get; set; }
    
    
    /// <summary>
    /// Название продукта.
    /// </summary>  
    public string Name {  get; set; } = string.Empty;

    /// <summary>
    /// Доступное количество на складе.
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Активно ли это предложение.
    /// Продавец может временно отключить оффер или убрать с витрины.
    /// </summary>
    public bool IsActive { get; set; } = true;
 

    // Навигационные свойства: 
    public virtual ProductVariant ProductVariant { get; set; } = null!;
 
    public virtual Seller Seller { get; set; } = null!;
    
    
    public virtual ICollection<SellerOfferImage> Images { get; set; } = new List<SellerOfferImage>();
}