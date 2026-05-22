using DomainApplication = JobBoard.Domain.Entities.Application;
using JobBoard.Application.Interfaces;
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

        public async Task<(IEnumerable<DomainApplication> Applications, int TotalCount)> GetByJobIdAsync(Guid jobId, int pageNumber, int pageSize)
        {
            var query = _context.Applications.AsQueryable();
            query = query.Include(a=>a.User).Where(a => a.JobId == jobId);
            int totalCount = await query.CountAsync();
            var applications = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (applications, totalCount);
        }

        public async Task<IEnumerable<DomainApplication>> GetByJobIdAsync(Guid jobId)
        {
            return await _context.Applications
            .Include(a=>a.User)
            .Where(a => a.JobId == jobId)
            .ToListAsync();
        }
        public async Task<(IEnumerable<DomainApplication> applications, int TotalCount)> GetByCandidateIdAsync(Guid candidateId, int pageNumber, int pageSize)
        {
            var query = _context.Applications.AsQueryable();
            query = query.Include(a=>a.Job).Where(a => a.CandidateId == candidateId);
            int totalCount = await query.CountAsync();
            var applications = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (applications, totalCount);
        }

        public async Task UpdateAsync(DomainApplication application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}
