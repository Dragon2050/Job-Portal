using MediatR;
using DomainApplication = JobBoard.Domain.Entities.Application;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Enums;
using JobBoard.Application.Interfaces;

namespace JobBoard.Application.Features.Applications.Commands
{
    public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, Guid>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;

        public ApplyToJobCommandHandler(IApplicationRepository applicationRepository, IJobRepository jobRepository, IUserRepository userRepository, IFileService fileService)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async Task<Guid> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
        {
            //1. Job Validation
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if(job == null) throw new Exception("Job not found");

            //2. Remove Duplicate Applications
            var existingApplications = await _applicationRepository.GetByJobIdAsync(request.JobId);
            if(existingApplications.Any(a=> a.CandidateId == request.CandidateId))
            {
                throw new Exception("You have already applied to this job");
            }

            // 3. CV Enforcement Logic
            var user = await _userRepository.GetByIdAsync(request.CandidateId);
            if (user == null) throw new Exception("User not found");
            
            string finalCvPath = string.Empty;
            if(request.FileData != null && request.FileData.Length > 0 && !string.IsNullOrEmpty(request.FileName))
            {
                // Option A: They uploaded a new file
                //Save the new file and get path
                finalCvPath = await _fileService.SaveFileAsync(request.FileData, request.FileName, "cvs");
                user.CVPath = finalCvPath;
            }
            else
            {
                //Option B: They don't upload a file. Use their existing Cv
                if(string.IsNullOrEmpty(user.CVPath))
                {
                    throw new Exception("You must upload a CV before applying");
                }
                finalCvPath = user.CVPath;
            }
            var application = new DomainApplication
            {
                Id = Guid.NewGuid(),
                JobId = request.JobId,
                CandidateId = request.CandidateId,
                Status = ApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                ResumePath = finalCvPath
            };
            await _applicationRepository.AddAsync(application);
            return application.Id;
        }
    }
}
