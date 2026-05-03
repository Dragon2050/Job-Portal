using JobBoard.Application.Features.Applications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetMyApplicationsQuery: IRequest<IEnumerable<CandidateApplicationDto>>
    {
        public Guid CandidateId { get; set; }
    }
}