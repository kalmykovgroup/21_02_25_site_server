using Application.Common;
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Responses
{
    public class ProductsMainPagedResponse : BaseResponse
    {
        /// <summary>
        /// Содержит коллекцию элементов для текущей страницы.
        /// </summary>
        public IEnumerable<ShortProductDto> Products { get; set; }

        public bool HasMoreProducts { get; }

        public IEnumerable<RecommendedGroupDto> RecommendedGroupsDto { get; set; }

        public bool HasMoreRecommendedGroups { get; } 

        /// <summary>
        /// Номер текущей страницы.
        /// Обычно начинается с 1.
        /// </summary>
        public int Page { get; }


        /// <param name="products">Коллекция элементов для текущей страницы.</param> 
        /// <param name="page">Номер текущей страницы.</param> 
        public ProductsMainPagedResponse(
            IEnumerable<ShortProductDto> products,
            bool hasMoreProducts,
            IEnumerable<RecommendedGroupDto> recommendedGroupsDto, 
            bool hasMoreRecommendedGroups,
            int page)
        {
            Products = products; 
            HasMoreProducts = hasMoreProducts;
            RecommendedGroupsDto = recommendedGroupsDto;
            HasMoreRecommendedGroups = hasMoreRecommendedGroups;
            Page = page;
        }
    }
}
