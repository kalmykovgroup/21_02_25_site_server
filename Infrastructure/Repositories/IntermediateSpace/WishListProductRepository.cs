using Domain.Entities.IntermediateSpace;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.IntermediateSpace
{
    public class WishListProductRepository : BaseRepository, IWishListProductRepository
    {
        public WishListProductRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<int> AddAsync(WishListProduct entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.WishListProducts.AddAsync(entity, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid wishListId, Guid productId, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.WishListProducts.Where(e => e.WishListId == wishListId && e.ProductId == productId).FirstOrDefaultAsync(cancellationToken);

            if (entity == null) return -1; // ✅ Если записи нет – выходим

            // ✅ Жёсткое удаление
            _dbContext.WishListProducts.Remove(entity);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid wishListId, Guid productId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.WishListProducts.AnyAsync(wl => wl.WishListId == wishListId && wl.ProductId == productId, cancellationToken);
        }
    }
}
