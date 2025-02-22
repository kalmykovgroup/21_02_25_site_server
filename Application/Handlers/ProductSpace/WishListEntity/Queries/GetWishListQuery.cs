using Application.Handlers.ProductSpace.WishListEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Queries
{
    public class GetWishListQuery : IRequest<GetWishListResponse>
    {
        public Guid WishListId { get; set; }

        public GetWishListQuery(Guid wishListId)
        {
            WishListId = wishListId;
        }
    }
}
