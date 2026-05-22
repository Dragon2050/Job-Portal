using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; } = string.Empty;
    }
}
