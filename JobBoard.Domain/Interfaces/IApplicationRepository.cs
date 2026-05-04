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
        Task AddAsync(Application application);
        Task<IEnumerable<Application>> GetByCandidateIdAsync(Guid CandidateId);
        Task UpdateAsync(Application application);
    }
}
