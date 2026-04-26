using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;

namespace JobBoard.Domain.Interfaces
{
    public interface IJobRepository
    {
        Task<Job?> GetByIdAsync(Guid id);
        Task<IEnumerable<Job>> GetAllAsync();
        Task AddAsync(Job job);
    }
}
