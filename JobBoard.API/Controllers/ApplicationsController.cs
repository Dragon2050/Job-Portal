using JobBoard.Application.Features.Applications.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Candidate")] // Only Candidates can apply!
        [HttpPost("apply/{jobId}")]
        public async Task<IActionResult> Apply(Guid jobId)
        {
            // 1. Extract the Candidate's ID securely from their JWT Token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token.");
            }
            
            var candidateId = Guid.Parse(userIdClaim.Value);

            // 2. Create the command
            var command = new ApplyToJobCommand
            {
                JobId = jobId,
                CandidateId = candidateId
            };

            // 3. Send it to the Handler we just wrote
            try
            {
                var applicationId = await _mediator.Send(command);
                return Ok(new { ApplicationId = applicationId, Message = "Successfully applied to job!" });
            }
            catch (Exception ex)
            {
                // If they already applied, or the job doesn't exist, our Handler throws an exception.
                // We catch it here and return a Bad Request.
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
