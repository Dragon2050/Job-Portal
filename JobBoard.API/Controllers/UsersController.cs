using MediatR;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Application.Features.Users.Queries;

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
}