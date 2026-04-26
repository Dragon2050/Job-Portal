using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using JobBoard.Domain.Entities;
using JobBoard.Application.Features.Jobs.DTOs;

namespace JobBoard.Application.Features.Jobs.Queries
{
    public class GetAllJobsQuery : IRequest<IEnumerable<JobResponseDto>>
    {

    }
}
