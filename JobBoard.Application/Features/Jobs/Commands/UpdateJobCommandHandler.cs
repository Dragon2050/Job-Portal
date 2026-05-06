using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using JobBoard.Domain.Interfaces;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMediator _mediator;
        public UpdateJobCommandHandler(IJobRepository jobRepository, IMediator mediator)
        {
            _jobRepository = jobRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if the job exists
            var existingJob = await _jobRepository.GetByIdAsync(request.JobId);
            if (existingJob == null)
            {
                throw new Exception("Job not found.");
                return false;
            }
            if(existingJob.CreatedById!=request.RecruiterId)
            {
                throw new UnauthorizedAccessException("This Recruiter doesn't create the Job");
                return false;
            }
            if(request.Title != null)
                existingJob.Title = request.Title;
            
            if(request.Description != null)
                existingJob.Description = request.Description;
            if(request.Location != null)
                existingJob.Location = request.Location;
            if(request.Salary.HasValue)
                existingJob.Salary = (decimal) request.Salary;
            if (request.CompanyName != null)
                existingJob.CompanyName = request.CompanyName;
            if (request.ApplicationDeadline.HasValue)
                existingJob.ApplicationDeadline = request.ApplicationDeadline;
                
            await _jobRepository.UpdateAsync(existingJob);
            return true;
        }
    }
}
