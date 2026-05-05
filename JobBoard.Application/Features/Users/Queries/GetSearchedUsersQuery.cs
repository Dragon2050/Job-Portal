using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Domain.Enums;
using MediatR;

namespace JobBoard.Application.Features.Users.Queries;
public class GetSearchedUsersQuery : IRequest<IEnumerable<UserDto>>
{
    public string? SearchTerm { get; set; }
    public Role? Role { get; set; }
}