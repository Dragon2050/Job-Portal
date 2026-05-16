using AutoMapper;
using MediatR;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Features.Applications.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobBoard.Application.Common;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, PagedResult<CandidateApplicationDto>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        public GetMyApplicationsQueryHandler(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<CandidateApplicationDto>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
        {
            //Fetch applications for candidate
            var (applications, totalCount) = await _applicationRepository.GetByCandidateIdAsync(request.CandidateId, request.PageNumber, request.PageSize);

            var dtos = _mapper.Map<IEnumerable<CandidateApplicationDto>>(applications);

            var result = new PagedResult<CandidateApplicationDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return result;
        }
        
    }
}