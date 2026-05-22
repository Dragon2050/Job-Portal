using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Jobs.DTOs
{
    public class JobResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
    }
}
