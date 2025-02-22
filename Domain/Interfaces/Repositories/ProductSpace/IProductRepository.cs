using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.ProductSpace
{
    public interface IProductRepository : IBaseRepository
    {

        Task<int> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);

        public Task<(IEnumerable<Product> Products, bool HasMore)> GetAllProductsAsync(
           string? search,
           Guid? categoryId,
           int page,
           CancellationToken cancellationToken = default);


    }
}
