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
 

        public Task<(IEnumerable<SellerOffer> SellerOffers, bool HasMore)> GetAllSellerOffersAsync(
           string? search,
           Guid? categoryId,
           int page,
           int firstPageSize,
           int nextPageSize,
           CancellationToken cancellationToken = default);
         
        
        Task<List<string>> GetProductNameSuggestionsAsync(string input, int limit = 10); 

    }
}
