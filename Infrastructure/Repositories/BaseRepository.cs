using Domain.Interfaces.Repositories.BaseInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly AppDbContext _dbContext; 
        ILogger<BaseRepository> _logger;

        public BaseRepository(AppDbContext dbContext, ILogger<BaseRepository> logger)
        {
            _dbContext = dbContext; 
            _logger = logger;
        }


        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.ChangeTracker.HasChanges())
            {
                _logger.LogError($"Попытка сохранить данные, при том что entity framework не видит изменения");
                return;

            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
