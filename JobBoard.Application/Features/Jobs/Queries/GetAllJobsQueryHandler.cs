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

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, IEnumerable<JobResponseDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetAllJobsQueryHandler(IJobRepository repository, IMapper mapper)
        {
            _jobRepository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<JobResponseDto>> Handle(
        GetAllJobsQuery request, 
        CancellationToken cancellationToken
        )
        {
            var jobs = await _jobRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobResponseDto>>(jobs);
        }
    }
}
