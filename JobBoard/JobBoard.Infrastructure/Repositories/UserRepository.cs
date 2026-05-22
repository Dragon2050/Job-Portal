using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using JobBoard.Application.Interfaces;
using JobBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository 
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u=> u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<User>, int TotalCount)> GetAllAsync(string searchTerm, Role? role, int pageNumber, int pageSize)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(u =>
                        u.FullName.ToLower().Contains(searchTerm) ||
                        u.Email.ToLower().Contains(searchTerm));
            }
            if (role.HasValue)
            {
                query = query.Where(u =>
                u.Role == role);
            }
            int totalCount = await query.CountAsync();
            var users = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return (users, totalCount);
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
