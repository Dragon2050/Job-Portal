using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Application.Features.Jobs.Commands;
using JobBoard.Application.Features.Jobs.Queries;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobCommand command)
        {
            var jobId = await _mediator.Send(command);
            return Ok(jobId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllJobsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetJobByIdQuery(id));
            return Ok(result);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpGet("my-created-jobs")]
        public async Task<IActionResult> GetMyCreatedJobs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }
            var recruiterId = Guid.Parse(userIdClaim.Value);
            var query = new GetMyJobsQuery
            {
                RecruiterId = recruiterId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] GetSearchedJobsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateJobCommand command)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }
            var recruiterId = Guid.Parse(userIdClaim.Value);
            command.RecruiterId = recruiterId;
            command.JobId = id;
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [Authorize(Roles = "Recruiter")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }
            var recruiterId = Guid.Parse(userIdClaim.Value);
            var command = new DeleteJobCommand
            {
                JobId = id,
                RecruiterId = recruiterId
            };
            try
            {
                var result = await _mediator.Send(command);
                return result ? Ok("Job deleted successfully") : NotFound("Job not found");
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
