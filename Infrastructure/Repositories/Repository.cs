/*using Castle.Core.Logging;
using Domain.Entities;
using Domain.Entities.Common.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity, TEntityKey> : IRepository<TEntity, TEntityKey> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        ILogger<Repository<TEntity, TEntityKey>> _logger;

        public Repository(AppDbContext dbContext, ILogger<Repository<TEntity, TEntityKey>> logger)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _logger = logger;
        }

        public virtual async Task<IEnumerable<TEntity>> GetParentCategoriesAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool includeDeleted = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            // ✅ Проверяем, является ли TEntity наследником IAuditable
            if (!includeDeleted && typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Cast<IAuditableEntity>().Where(e => !e.IsDeleted).Cast<TEntity>();
            }

            // ✅ Применяем предикат, если передан
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync(cancellationToken);
        }


        // ✅ Метод для поиска по предикату
        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<TEntity?> GetByKeyAsync(TEntityKey key)
        {
            if (key is ITuple compositeKey)
            {
                return await _dbSet.FindAsync(Enumerable.Range(0, compositeKey.Length)
                             .Select(i => compositeKey[i]).ToArray());
            }
            return await _dbSet.FindAsync(key);
        }
         



        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken); 
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }


        // ✅ Метод для проверки существования записи по условию
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);
        }


        /// <summary>
        /// Удаление одной записи по ключу.
        /// </summary>
        public virtual async Task DeleteByKeyAsync(TEntityKey key, bool softDeleted = true)
        {
            TEntity? entity = await GetByKeyAsync(key);

            if (entity == null)
            {
                _logger.LogWarning($"Попытка удалить элемент, который не найден по ключу: {key}");
                return;
            };
            await DeleteEntityAsync(entity, softDeleted);
        }



        /// <summary>
        /// Удаление записей по предикату (можно указать `take`, чтобы удалить ограниченное количество).
        /// </summary>
        public virtual async Task DeleteByPredicateAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool softDelete = true,
            int? take = null)
        {
            IQueryable<TEntity> query = _dbSet.Where(predicate);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            var entities = await query.ToListAsync();
            foreach (var entity in entities)
            {
                await DeleteEntityAsync(entity, softDelete);
            }
        } 

        /// <summary>
        /// Вспомогательный метод для удаления объекта (мягко или полностью).
        /// </summary>
        public virtual Task DeleteEntityAsync(TEntity entity, bool softDelete)
        {
            if (softDelete && entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.IsDeleted = true;
            }
            else
            {
                _dbSet.Remove(entity);
            }

            return Task.CompletedTask;
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
*/