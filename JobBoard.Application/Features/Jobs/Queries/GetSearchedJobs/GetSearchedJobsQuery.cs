using JobBoard.Application.Common;
using JobBoard.Application.Features.Jobs.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetSearchedJobsQuery : IRequest<PagedResult<RecruiterJobDto>>
    {
        public string? searchTerm { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        //Default page 1 with 10 items.
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
