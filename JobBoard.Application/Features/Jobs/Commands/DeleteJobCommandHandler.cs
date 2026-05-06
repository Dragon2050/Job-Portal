using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Interfaces;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMediator _mediator;
        public DeleteJobCommandHandler(IJobRepository jobRepository, IMediator mediator)
        {
            _jobRepository = jobRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var existingJob = await _jobRepository.GetByIdAsync(request.JobId);
            if (existingJob == null)
            {
                throw new Exception("Job not found.");
                return false;
            }
            if (existingJob.CreatedById != request.RecruiterId)
            {
                throw new UnauthorizedAccessException("This Recruiter doesn't create the Job");
                return false;
            }
            await _jobRepository.DeleteAsync(existingJob);
            return true;
        }
    }
}
