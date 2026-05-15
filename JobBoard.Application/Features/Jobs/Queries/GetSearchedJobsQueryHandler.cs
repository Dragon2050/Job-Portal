using JobBoard.Application.Features.Jobs.DTOs;
using JobBoard.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobBoard.Application.Common;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetSearchedJobsQueryHandler : IRequestHandler<GetSearchedJobsQuery, PagedResult<RecruiterJobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetSearchedJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<RecruiterJobDto>> Handle(GetSearchedJobsQuery request, CancellationToken cancellationToken)
        {
            var (jobs, totalCount) = await _jobRepository.GetAllAsync(request.searchTerm, request.MinSalary, request.MaxSalary, request.PageNumber, request.PageSize);
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
