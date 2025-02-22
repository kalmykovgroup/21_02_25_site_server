using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using Domain.Interfaces.Repositories.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.UserSpace
{
    public interface IPhoneVerificationCodeRepository : IBaseRepository
    {
        Task<PhoneVerificationCode?> GetByPhoneNumberAsync(string phoneNumber);

        Task<int> AddAsync(PhoneVerificationCode entity, CancellationToken cancellationToken = default);

        Task<int> UpdateAsync(PhoneVerificationCode entity, CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
