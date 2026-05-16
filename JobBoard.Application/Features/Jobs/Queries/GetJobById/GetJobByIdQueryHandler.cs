using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using JobBoard.Domain.Entities;
using JobBoard.Application.Interfaces;
using JobBoard.Application.Features.Jobs.DTOs;
using AutoMapper;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobResponseDto?>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetJobByIdQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<JobResponseDto?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.Id);
            return _mapper.Map<JobResponseDto>(job);
        }
    }
}
