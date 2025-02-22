using Application.Common;
using Application.Handlers.ProductSpace.WishListEntity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Responses
{
    public class GetWishListResponse : BaseResponse
    {
        public List<WishListItemDto> WishList { get; set; } = null!;
    }
}
