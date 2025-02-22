using Domain.Entities.ProductSpace;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IntermediateSpace
{
    public class WishListProduct
    {
        /// <summary>
        /// Уникальный идентификатор продукта, связанного с тегом.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор тега, связанного с продуктом.
        /// </summary> 
        public Guid WishListId { get; set; }

        /// <summary>
        /// Ссылка на тег.
        /// </summary>
        public virtual WishList WishList { get; set; } = null!;
    }
}
