using Domain.Entities.ProductSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.BaseInterfaces
{
    internal interface IAddRepository<TEntity> : IBaseRepository
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
