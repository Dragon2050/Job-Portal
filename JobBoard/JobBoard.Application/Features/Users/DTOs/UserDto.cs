using JobBoard.Domain.Enums;
namespace JobBoard.Application.Features.Users.DTOs;
public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}