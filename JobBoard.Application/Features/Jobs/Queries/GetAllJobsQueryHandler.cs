using JobBoard.Application.Features.Jobs.DTOs;
using JobBoard.Domain.Entities;
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
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, PagedResult<JobResponseDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetAllJobsQueryHandler(IJobRepository repository, IMapper mapper)
        {
            _jobRepository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<JobResponseDto>> Handle(
        GetAllJobsQuery request, 
        CancellationToken cancellationToken
        )
        {
            var (jobs, totalCounts) = await _jobRepository.GetAllAsync(request.PageNumber, request.PageSize);
            
            var dtos = _mapper.Map<IEnumerable<JobResponseDto>>(jobs);
            return new PagedResult<JobResponseDto>
            {
                Items = dtos,
                TotalCount = totalCounts,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
