using Castle.Core.Logging;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using Domain.Interfaces.Repositories.UserSpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserSpace
{
    public class PhoneVerificationCodeRepository : BaseRepository, IPhoneVerificationCodeRepository
    { 
        public PhoneVerificationCodeRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger) { }

        public async Task<int> AddAsync(PhoneVerificationCode entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.PhoneVerificationCodes.AddAsync(entity, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.PhoneVerificationCodes.Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

            if (entity == null) return -1; // ✅ Если записи нет – выходим

            // ✅ Жёсткое удаление
            _dbContext.PhoneVerificationCodes.Remove(entity);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PhoneVerificationCode?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbContext.PhoneVerificationCodes.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<int> UpdateAsync(PhoneVerificationCode phoneVerificationCode, CancellationToken cancellationToken = default)
        {
            var existingPhoneVerificationCode= await _dbContext.PhoneVerificationCodes.FirstOrDefaultAsync(p => p.Id == phoneVerificationCode.Id, cancellationToken);

            if (existingPhoneVerificationCode == null)
            {
                return -1; // ❌ Продукт не найден
            }

            // ✅ Обновляем только измененные поля
            _dbContext.Entry(existingPhoneVerificationCode).CurrentValues.SetValues(phoneVerificationCode);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
