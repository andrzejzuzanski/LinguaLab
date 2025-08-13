using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<int> SaveChangesAsync();
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
