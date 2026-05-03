using AutoMapper;
using MediatR;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Features.Applications.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetApplicationsForJobQueryHandler : IRequestHandler<GetApplicationsForJobQuery, IEnumerable<ApplicationResponseDto>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetApplicationsForJobQueryHandler(IApplicationRepository applicationRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationResponseDto>> Handle(GetApplicationsForJobQuery request, CancellationToken CancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if(job==null)
            {
                throw new Exception("Job not found");
            }

            if(job.CreatedById != request.RecruiterId)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            var applications = await _applicationRepository.GetByJobIdAsync(request.JobId);
            return _mapper.Map<IEnumerable<ApplicationResponseDto>>(applications);
        }
    }
}