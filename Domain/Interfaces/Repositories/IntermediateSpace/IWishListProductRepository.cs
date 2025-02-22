using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.IntermediateSpace
{
    public interface IWishListProductRepository : IBaseRepository
    {
        Task<int> AddAsync(WishListProduct entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(Guid wishListId, Guid productId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid wishListId, Guid productId, CancellationToken cancellationToken = default);
    }
}
