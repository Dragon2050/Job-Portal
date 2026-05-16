using MediatR;
using System;
using JobBoard.Application.Features.Applications.DTOs;
using System.Collections.Generic;
using JobBoard.Application.Common;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetApplicationsForJobQuery : IRequest<PagedResult<ApplicationResponseDto>>
    {
        public Guid JobId {get; set;}
        public Guid RecruiterId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}