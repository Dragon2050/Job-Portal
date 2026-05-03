using AutoMapper;
using MediatR;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Features.Applications.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, IEnumerable<CandidateApplicationDto>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        public GetMyApplicationsQueryHandler(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CandidateApplicationDto>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
        {
            //Fetch applications for candidate
            var applications = await _applicationRepository.GetByCandidateIdAsync(request.CandidateId);

            return _mapper.Map<IEnumerable<CandidateApplicationDto>>(applications);
        }
        
    }
}