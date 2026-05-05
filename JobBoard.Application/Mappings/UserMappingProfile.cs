using AutoMapper;
using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Domain.Entities;

namespace JobBoard.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}