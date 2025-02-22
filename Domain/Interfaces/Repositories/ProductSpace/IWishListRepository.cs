using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.ProductSpace
{
    public interface IWishListRepository : IBaseRepository 
    {
        Task<int> AddAsync(WishList entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetWishListProductsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
