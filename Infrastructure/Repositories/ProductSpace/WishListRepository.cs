using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductSpace
{
    public class WishListRepository : BaseRepository, IWishListRepository
    {
        public WishListRepository(AppDbContext dbContext, ILogger<WishListRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<int> AddAsync(WishList entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.WishLists.AddAsync(entity, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.WishLists.Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (entity == null) return -1; // ✅ Если записи нет – выходим

            // ✅ Жёсткое удаление
            _dbContext.WishLists.Remove(entity);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Product>> GetWishListProductsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var wishList = await _dbContext.WishLists
                 .Where(wl => wl.Id == id)
                 .Include(wl => wl.WishListProducts) // Загружаем связи
                 .ThenInclude(wlp => wlp.Product)    // Загружаем сами продукты
                 .FirstOrDefaultAsync(cancellationToken);

            if (wishList == null)
                throw new Exception("Wish list not found");

            return wishList.WishListProducts
                .Select(wlp => wlp.Product)
                .ToList(); // Достаём только список продуктов
        }
    }
}
