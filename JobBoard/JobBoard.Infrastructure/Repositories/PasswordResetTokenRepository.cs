using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
using JobBoard.Application.Interfaces;
using JobBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
    public class PasswordResetTokenRepository: IPasswordResetTokenRepository
    {
        private readonly ApplicationDbContext _context;
        public PasswordResetTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Domain.Entities.PasswordResetToken token)
        {
            await _context.PasswordResetTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        public async Task<PasswordResetToken> GetByTokenAsync(string token)
        {
            return await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
        public async Task DeleteAsync(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }
}
