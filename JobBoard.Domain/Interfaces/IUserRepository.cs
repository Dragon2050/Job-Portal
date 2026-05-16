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
        Task<(IEnumerable<User>, int TotalCount)> GetAllAsync(string? searchTerm, Role? role, int pageNumber, int pageSize);
        Task UpdateAsync(User user);
    }
}
