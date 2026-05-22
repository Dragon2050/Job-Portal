using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using JobBoard.Domain.Entities;
using JobBoard.Application.Features.Jobs.DTOs;
using JobBoard.Application.Common;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetAllJobsQuery : IRequest<PagedResult<JobResponseDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
