/*using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TEntityKey>
        where TEntity : class
    {

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Получить все записи с возможной фильтрацией.
        /// </summary>
        /// <param name="includeDeleted">`true` – Включить записи, который были удалены, `false` – Удаленные записи не будут включены (По умолчанию).</param>
        Task<IEnumerable<TEntity>> GetParentCategoriesAsync(Expression<Func<TEntity, bool>>? predicate = null, bool includeDeleted = false, CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByKeyAsync(TEntityKey key);



        Task UpdateAsync(TEntity entity);


        /// <param name="key">Ключ для удаления одной записи (если `predicate == null`).</param>
        /// <param name="softDelete">`true` – мягкое удаление, `false` – полное удаление.</param>
        Task DeleteByKeyAsync(TEntityKey key, bool softDeleted = true);

        Task DeleteByPredicateAsync(Expression<Func<TEntity, bool>> predicate, bool softDelete = true, int? take = null);

        Task DeleteEntityAsync(TEntity entity, bool softDelete);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
*/