using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using JobBoard.Domain.Interfaces;
using MediatR;

namespace JobBoard.Application.Features.Applications.Commands
{
    public class ApplyToJobCommand : IRequest<Guid>
    {
        public Guid JobId { get; set;}
        public Guid CandidateId { get; set;}
    }
}