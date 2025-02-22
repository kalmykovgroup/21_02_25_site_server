using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.UserSpace;

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Список желаний клиента
    /// </summary> 
    public class WishList
    {

        public Guid Id { get; set; }
          
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Товары в списке желаний
        /// Коллекция для связи многие ко многим
        /// </summary>
        public virtual ICollection<WishListProduct> WishListProducts { get; set; } = new List<WishListProduct>();

    }

}
