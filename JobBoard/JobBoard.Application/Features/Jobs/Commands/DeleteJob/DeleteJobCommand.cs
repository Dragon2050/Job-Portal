using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public Guid JobId { get; set; }
        public Guid RecruiterId { get; set; }
    }
}
