using Application.Common;
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Responses
{
    /// <summary>
    /// Класс для представления результатов постраничной выборки товаров.
    /// Содержит список элементов для текущей страницы, общее количество элементов,
    /// номер текущей страницы и эффективный размер страницы (количество элементов, 
    /// возвращаемых на данной странице).
    /// </summary>
    public class ProductsPagedResponse : BaseResponse
    {
        /// <summary>
        /// Содержит коллекцию элементов для текущей страницы.
        /// </summary>
        public IEnumerable<ShortSellerOfferDto> Items { get; }

        /// <summary>
        ///  Есть ли еще записи 
        /// </summary>
        public bool HasMore { get; }

        /// <summary>
        /// Номер текущей страницы.
        /// Обычно начинается с 1.
        /// </summary>
        public int Page { get; }

         
        /// <param name="items">Коллекция элементов для текущей страницы.</param>
        /// <param name="hasMore">Есть ли еще записи.</param>
        /// <param name="page">Номер текущей страницы.</param> 
        public ProductsPagedResponse(IEnumerable<ShortSellerOfferDto> items, bool hasMore, int page)
        {
            Items = items;
            HasMore = hasMore;
            Page = page; 
        }
    }


}
