using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
using DomainApplication = JobBoard.Domain.Entities.Application;

namespace JobBoard.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<DomainApplication?> GetByIdAsync(Guid id);
        Task<IEnumerable<DomainApplication>> GetByJobIdAsync(Guid jobId);
        Task<(IEnumerable<DomainApplication> Applications, int TotalCount)> GetByJobIdAsync(Guid jobId, int pageNumber, int pageSize);
        Task AddAsync(DomainApplication application);
        Task<(IEnumerable<DomainApplication> applications, int TotalCount)> GetByCandidateIdAsync(Guid CandidateId, int pageNumber, int pageSize);
        Task UpdateAsync(DomainApplication application);
    }
}
