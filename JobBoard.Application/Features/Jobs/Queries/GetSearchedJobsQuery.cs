using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Features.Jobs.DTOs;
using MediatR;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetSearchedJobsQuery: IRequest<IEnumerable<RecruiterJobDto>>
    {
        public string? searchTerm { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }
}
