using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Interfaces;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class CreateJobCommandHandler
    : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IJobRepository _jobRepository;

        public CreateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Guid> Handle(
            CreateJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = new Job
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Salary = request.Salary,
                CreatedById = request.CreatedById
            };
            await _jobRepository.AddAsync(job);
            return job.Id;
        }
    }
}
