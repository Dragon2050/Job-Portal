using DomainApplication = JobBoard.Domain.Entities.Application;
using JobBoard.Domain.Interfaces;
using JobBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DomainApplication application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task<DomainApplication?> GetByIdAsync(Guid id)
        {
            return await _context.Applications
            .Include(a=>a.Job)
            .FirstOrDefaultAsync(a=> a.Id==id);
        }

        public async Task<IEnumerable<DomainApplication>> GetByJobIdAsync(Guid jobId)
        {
            return await _context.Applications.Include(a=>a.User)
            .Where(a=> a.JobId == jobId)
            .ToListAsync();
        }
        public async Task<IEnumerable<DomainApplication>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _context.Applications
            .Include(a=>a.Job)
            .Where(a=>a.CandidateId == candidateId)
            .ToListAsync();
        }

        public async Task UpdateAsync(DomainApplication application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}