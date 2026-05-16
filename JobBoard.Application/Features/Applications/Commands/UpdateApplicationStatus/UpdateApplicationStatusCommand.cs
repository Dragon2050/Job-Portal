using JobBoard.Domain.Enums;
using MediatR;
using System;

namespace JobBoard.Application.Features.Applications.Commands
{
    public class UpdateApplicationStatusCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public ApplicationStatus Status { get; set; }
        public Guid RecruiterId { get; set; }
    }
}