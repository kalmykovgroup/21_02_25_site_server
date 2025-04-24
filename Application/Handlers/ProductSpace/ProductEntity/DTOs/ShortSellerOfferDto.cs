using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.DTOs
{
    public class ShortSellerOfferDto
    {
        public Guid Id { get; set; } 
        public Guid SellerId { get; set; } 
        public Guid ProductVariantId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public decimal? OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfReviews { get; set; }
        public decimal? DiscountPercentage { get; set; }

        public bool IsDiscount => OriginalPrice != null && DiscountPercentage != null;

        public List<SellerOfferImageDto> Images { get; set; } = new();

    }
}
