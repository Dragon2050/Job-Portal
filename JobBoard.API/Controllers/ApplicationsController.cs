using JobBoard.Application.Features.Applications.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JobBoard.Application.Features.Applications.Queries;
using JobBoard.Application.Features.Applications.DTOs;

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
        public async Task<IActionResult> Apply(Guid jobId, IFormFile? cvFile)
        {
            // 1. Extract the Candidate's ID securely from their JWT Token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token.");
            }
            
            var candidateId = Guid.Parse(userIdClaim.Value);
            using var memoryStream = new MemoryStream();

            //If they provide a file, convert it to byte[]
            if(cvFile != null && cvFile.Length > 0)
            {
                if(Path.GetExtension(cvFile.FileName).ToLower() != ".pdf")
                {
                    return BadRequest(new { Error = "Only PDF files are allowed" });
                }
                await cvFile.CopyToAsync(memoryStream);
            }

            // 2. Create the command
            var command = new ApplyToJobCommand
            {
                JobId = jobId,
                CandidateId = candidateId,
                FileData = memoryStream.ToArray(),
                FileName = cvFile.FileName
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

        // JobBoard.API/Controllers/ApplicationsController.cs
        // Update the GetApplicationsForJob method:

        [Authorize(Roles = "Recruiter")]
        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetApplicationsForJob(Guid jobId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }

            var recruiterId = Guid.Parse(userIdClaim.Value);

            try
            {
                var query = new GetApplicationsForJobQuery
                {
                    JobId = jobId,
                    RecruiterId = recruiterId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Returns 403 Forbidden
                return StatusCode(403, new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [Authorize(Roles = "Candidate")]
        [HttpGet("my-applications")]
        public async Task<IActionResult> GetMyApplications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if(userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }
            var candidateId = Guid.Parse(userIdClaim.Value);
            
            var query = new GetMyApplicationsQuery
            {
                CandidateId = candidateId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            
            try{
                var applications = await _mediator.Send(query);
                return Ok(applications);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateApplicationStatusRequestDto request)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }
            var recruiterId = Guid.Parse(userIdClaim.Value);

            var Command = new UpdateApplicationStatusCommand
            {
                ApplicationId = id,
                Status = request.status,
                RecruiterId = recruiterId
            };
            try
            {
                await _mediator.Send(Command);
                return NoContent();
            }
            catch(UnauthorizedAccessException ex)
            {
                // Returns 403 Forbidden
                return StatusCode(403, new { Error = ex.Message });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
