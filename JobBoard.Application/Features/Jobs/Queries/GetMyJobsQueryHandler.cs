using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Features.Jobs.DTOs;
using AutoMapper;
using JobBoard.Domain.Interfaces;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetMyJobsQueryHandler : IRequestHandler<GetMyJobsQuery, IEnumerable<RecruiterJobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetMyJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecruiterJobDto>> Handle(GetMyJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobsByRecruiterIdAsync(request.RecruiterId);
            return _mapper.Map<IEnumerable<RecruiterJobDto>>(jobs);
        }
    }
}
