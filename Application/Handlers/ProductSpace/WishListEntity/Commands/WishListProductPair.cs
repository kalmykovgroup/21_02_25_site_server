using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Commands
{
    public class WishListProductPair
    {
        public Guid ProductId {  get; set; }
        public bool IsFavorite { get; set; }
    }
}
