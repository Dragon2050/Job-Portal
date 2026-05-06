using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class UpdateJobCommand : IRequest<bool>
    {
        public Guid JobId { get; set; }
        public Guid RecruiterId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? CompanyName {get; set; }
        public DateTime? ApplicationDeadline {get; set;}
        public decimal? Salary { get; set; }
    }
}
