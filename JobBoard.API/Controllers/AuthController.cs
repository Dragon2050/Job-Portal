using JobBoard.Application.Features.Auth.Commands;
using JobBoard.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace JobBoard.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var id = await _mediator.Send(new RegisterCommand
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            });
            return Ok(new { id });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            string token = await _mediator.Send(new LoginCommand
            {
                Email = request.Email,
                Password = request.Password
            });
            return Ok(new { token });
        }
    }
}
