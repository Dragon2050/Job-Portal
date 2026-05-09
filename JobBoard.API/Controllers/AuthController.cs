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
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {

            await _mediator.Send(command);
            return Ok(new { message = "If an account exists with that email, a reset link has been sent." });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Password reset successfully. You can now log in." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
