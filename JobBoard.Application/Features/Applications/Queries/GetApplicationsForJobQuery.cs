using MediatR;
using System;
using JobBoard.Application.Features.Applications.DTOs;
using System.Collections.Generic;

namespace JobBoard.Application.Features.Applications.Queries
{
    public class GetApplicationsForJobQuery : IRequest<IEnumerable<ApplicationResponseDto>>
    {
        public Guid JobId {get; set;}
        public Guid RecruiterId { get; set; }
    }
}