using Domain.Entities.UserSpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Repositories.UserSpace;
using Domain.Entities.ProductSpace;

namespace Infrastructure.Repositories.UserSpace
{
    public class UserRepository : BaseRepository, IUserRepository
    { 

        public UserRepository(AppDbContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {
             
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "user не может быть null");
            }

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user;
        }
         

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);

        }
    }
}
