using JobBoard.Application.Common;
using JobBoard.Application.Features.Applications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetMyApplicationsQuery: IRequest<PagedResult<CandidateApplicationDto>>
    {
        public Guid CandidateId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}