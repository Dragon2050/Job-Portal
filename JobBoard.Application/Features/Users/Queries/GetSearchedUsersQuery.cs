using JobBoard.Application.Common;
using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Domain.Enums;
using MediatR;

namespace JobBoard.Application.Features.Users.Queries;
public class GetSearchedUsersQuery : IRequest<PagedResult<UserDto>>
{
    public string? SearchTerm { get; set; }
    public Role? Role { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}