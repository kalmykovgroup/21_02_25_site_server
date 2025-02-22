using Application.Handlers.ProductSpace.WishListEntity.Responses;
using Application.Handlers.ProductSpace.WishListEntity.Validator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Commands
{
    public class AddWishListProductCommand : IRequest<AddWishListProductResponse>
    {
        public List<WishListProductPair> Batch { get; set; }
        public Guid WishListId { get; set; }

        public AddWishListProductCommand(List<WishListProductPair> batch, Guid wishListId)
        {
            Batch = batch;
            WishListId = wishListId;
        }

        public class Validator : WishListProductValidator<AddWishListProductCommand> { }
    }
}
