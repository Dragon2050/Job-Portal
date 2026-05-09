using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync(string? searchTerm, Role? role);
        Task UpdateAsync(User user);
    }
}
