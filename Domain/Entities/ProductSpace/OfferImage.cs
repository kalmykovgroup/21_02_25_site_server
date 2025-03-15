using Domain.Entities.Common;

namespace Domain.Entities.ProductSpace;

public class OfferImage : AuditableEntity<OfferImage>
{
    public Guid Id { get; set; }
    public Guid SellerOfferId { get; set; }

    /// <summary>
    /// Порядковый номер изображения (1 — главное)
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Расширение оригинального файла (например, jpg, png)
    /// </summary>
    public string OriginalExtension { get; set; } = string.Empty;

    /// <summary>
    /// Путь к папке, где хранятся изображения (без размеров)
    /// </summary>
    public string StoragePath { get; set; } = string.Empty;

    public virtual SellerOffer SellerOffer { get; set; } = null!;

    /// <summary>
    /// Генерирует URL для изображения нужного размера
    /// </summary>
    public string GetImageUrl(string size, string baseUrl)
    {
        return $"{baseUrl}/{StoragePath}/{size}.{OriginalExtension}";
    }
}