using JobBoard.Application.Features.Jobs.DTOs;
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
    public class GetSearchedJobsQueryHandler : IRequestHandler<GetSearchedJobsQuery, IEnumerable<RecruiterJobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetSearchedJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecruiterJobDto>> Handle(GetSearchedJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync(request.searchTerm, request.MinSalary, request.MaxSalary);
            return _mapper.Map<IEnumerable<RecruiterJobDto>>(jobs);
        }
    }
}
