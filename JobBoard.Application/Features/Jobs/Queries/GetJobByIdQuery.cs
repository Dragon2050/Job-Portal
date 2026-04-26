using JobBoard.Application.Features.Jobs.DTOs;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Queries
{
    
    public class GetJobByIdQuery : IRequest<JobResponseDto?>
    {
        public Guid Id { get; set; }
        public GetJobByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
