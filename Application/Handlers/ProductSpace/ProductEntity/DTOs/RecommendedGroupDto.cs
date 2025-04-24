using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.DTOs
{
    public class RecommendedGroupDto 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Background { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public IEnumerable<ShortSellerOfferDto> Products { get; set; } = null!;
    }
}
