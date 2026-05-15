using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Features.Jobs.DTOs;
using AutoMapper;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Common;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetMyJobsQueryHandler : IRequestHandler<GetMyJobsQuery, PagedResult<RecruiterJobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetMyJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<RecruiterJobDto>> Handle(GetMyJobsQuery request, CancellationToken cancellationToken)
        {
            var (jobs, totalCount) = await _jobRepository.GetJobsByRecruiterIdAsync(request.RecruiterId, request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<RecruiterJobDto>>(jobs);
            return new PagedResult<RecruiterJobDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
