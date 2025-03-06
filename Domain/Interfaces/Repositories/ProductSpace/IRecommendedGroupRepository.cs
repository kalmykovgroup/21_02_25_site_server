using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.ProductSpace
{
    public interface IRecommendedGroupRepository : IBaseRepository
    {
        Task<(IEnumerable<RecommendedGroup>, bool HasMore)> GetGroups(int page, int firstPageSize, int nextPageSize);
    }
}
