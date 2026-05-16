using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;

namespace JobBoard.Application.Interfaces
{
    public interface IJobRepository
    {
        Task<Job?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Job> Jobs, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Job job);
        Task<(IEnumerable<Job> Jobs, int TotalCount)> GetJobsByRecruiterIdAsync(Guid recruiterId, int pageNumber, int pageSize);
        Task<(IEnumerable<Job> Jobs, int TotalCount)> GetAllAsync(string? searchTerm, decimal? minSalary, decimal? maxSalary, int pageNumber, int pageSize);
        Task UpdateAsync(Job job);
        Task DeleteAsync(Job job);
    }
}
