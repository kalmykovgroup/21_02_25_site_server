using Domain.Entities.Common;

namespace Domain.Entities.ProductSpace;

 
/// <summary>
/// Продавец на маркетплейсе.
/// </summary>
public class Seller: AuditableEntity<Seller>
{
    public Guid Id { get; set; }

    /// <summary>
    /// Имя или название магазина.
    /// </summary> 
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Флаг активности продавца.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Рейтинг продавца (1..5 или любой другой масштаб).
    /// </summary>
    public decimal Rating { get; set; } = 0.0m;
    
    // Навигационное свойство: один продавец может иметь много предложений (SellerOffer).
    public virtual ICollection<SellerOffer> SellerOffers { get; set; } = new List<SellerOffer>();
}