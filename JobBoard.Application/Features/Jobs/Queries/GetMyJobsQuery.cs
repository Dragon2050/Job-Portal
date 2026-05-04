using JobBoard.Application.Features.Jobs.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetMyJobsQuery : IRequest<IEnumerable<RecruiterJobDto>>
    {
        public Guid RecruiterId { get; set; }
    }
}
