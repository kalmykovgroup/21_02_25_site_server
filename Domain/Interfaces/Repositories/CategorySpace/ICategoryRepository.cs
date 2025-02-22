using Domain.Entities.CategorySpace;
using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.CategorySpace
{
    public interface ICategoryRepository : IBaseRepository
    {
        Task<IEnumerable<Category>> GetParentCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
