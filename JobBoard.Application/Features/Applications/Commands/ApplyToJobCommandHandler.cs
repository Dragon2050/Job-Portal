using MediatR;
using DomainApplication = JobBoard.Domain.Entities.Application;
using JobBoard.Domain.Interfaces;
using JobBoard.Domain.Enums;

namespace JobBoard.Application.Features.Applications.Commands
{
    public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, Guid>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;

        public ApplyToJobCommandHandler(IApplicationRepository applicationRepository, IJobRepository jobRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
        }

        public async Task<Guid> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if(job == null) throw new Exception("Job not found");
            var existingApplications = await _applicationRepository.GetByJobIdAsync(request.JobId);
            if(existingApplications.Any(a=> a.CandidateId == request.CandidateId))
            {
                throw new Exception("You have already applied to this job");
            }
            
            var application = new DomainApplication
            {
                Id = Guid.NewGuid(),
                JobId = request.JobId,
                CandidateId = request.CandidateId,
                Status = ApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow
            };
            await _applicationRepository.AddAsync(application);
            return application.Id;
        }
    }
}