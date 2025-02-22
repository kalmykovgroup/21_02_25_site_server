using Application.Common;
using Domain.Entities.IntermediateSpace;
using FluentValidation;

namespace Application.Handlers.ProductSpace.WishListEntity.Validator
{
    public class WishListProductValidator<TCommand> : DynamicValidator<TCommand>
    {

        protected override void ConfigureRules()
        {
            AddRule<Guid>(nameof(WishListProduct.ProductId), rule => rule
             .NotNull()
             .NotEqual(Guid.Empty).WithMessage("ProductId не может быть пустым GUID."));

            AddRule<Guid>(nameof(WishListProduct.WishListId), rule => rule
             .NotNull()
             .NotEqual(Guid.Empty).WithMessage("WishListId не может быть пустым GUID."));

        }
    }
}
