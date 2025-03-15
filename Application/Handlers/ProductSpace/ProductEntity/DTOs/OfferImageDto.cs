namespace Application.Handlers.ProductSpace.ProductEntity.DTOs;

public class OfferImageDto
{
    public int Index { get; set; }
    
    // ["100x100"] = $"{BaseImageUrl}/{img.StoragePath}/100x100.{img.OriginalExtension}",
    public Dictionary<string, string> Urls { get; set; } = new();
}