using Domain.Entities.Common.Interfaces;
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
    public interface IUserRepository : IBaseRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneAsync(string phone);

        Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
    }
}
