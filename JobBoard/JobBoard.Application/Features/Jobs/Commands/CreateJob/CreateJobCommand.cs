using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class CreateJobCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location {  get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime? ApplicationDeadline {get; set;}
        public Guid CreatedById { get; set; }
    }
}
