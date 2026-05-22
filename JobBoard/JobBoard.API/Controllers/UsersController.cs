using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using JobBoard.Application.Features.Users.Commands;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] GetSearchedUsersQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [Authorize(Roles = "Candidate")]
    [HttpPost("upload-cv")]
    public async Task<IActionResult> UploadCv(IFormFile file)
    {
        if(file==null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }
        // Optional: Restrict file types to PDF only
        if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            return BadRequest("Only PDF files are allowed.");
        // Optional: Restrict file size (e.g., Max 5 MB)
        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("File size cannot exceed 5MB.");
        //Extract user id from jwt
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if(userIdClaim == null)
        {
            return Unauthorized();
        }
        var userId = Guid.Parse(userIdClaim.Value);
        // Convert IFormFile to byte array
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileData = memoryStream.ToArray();

        var command = new UploadCvCommand
        {
            UserId = userId,
            FileData = fileData,
            FileName = file.FileName
        };
        
        var savedPath = await _mediator.Send(command);

        // Return the path to the caller (frontend)
        return Ok(new { Message = "CV uploaded successfully", Path = savedPath });
    }
}