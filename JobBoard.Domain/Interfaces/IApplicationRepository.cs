using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;

namespace JobBoard.Domain.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application?> GetByIdAsync(Guid id);
        Task<IEnumerable<Application>> GetByJobIdAsync(Guid jobId);
        Task<(IEnumerable<Application> Applications, int TotalCount)> GetByJobIdAsync(Guid jobId, int pageNumber, int pageSize);
        Task AddAsync(Application application);
        Task<(IEnumerable<Application> applications, int TotalCount)> GetByCandidateIdAsync(Guid CandidateId, int pageNumber, int pageSize);
        Task UpdateAsync(Application application);
    }
}
