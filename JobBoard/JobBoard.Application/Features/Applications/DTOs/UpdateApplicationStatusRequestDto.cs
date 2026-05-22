using JobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Applications.DTOs
{
    public class UpdateApplicationStatusRequestDto
    {
        public ApplicationStatus status { get; set; }
    }
}
