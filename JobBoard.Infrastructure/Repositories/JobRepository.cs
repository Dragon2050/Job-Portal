using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using JobBoard.Domain.Interfaces;
using JobBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
    public class JobRepository: IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job?> GetByIdAsync(Guid id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<Job?>> GetJobsByRecruiterIdAsync(Guid recruiterId)
        {
            return await _context.Jobs
                .Where(j=>j.CreatedById == recruiterId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job?>> GetAllAsync(string? searchTerm, decimal? minSalary, decimal? maxSalary)
        {
            var query = _context.Jobs.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(j => 
                                    j.Title.ToLower().Contains(searchTerm) || 
                                    j.Description.ToLower().Contains(searchTerm) ||
                                    j.Location.ToLower().Contains(searchTerm));
            }

            if (minSalary.HasValue)
            {
                query = query.Where(j => j.Salary >= minSalary.Value);
            }

            if (maxSalary.HasValue)
            {
                query = query.Where(j => j.Salary <= maxSalary.Value);
            }

            return await query.ToListAsync();
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Job job)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }
}
