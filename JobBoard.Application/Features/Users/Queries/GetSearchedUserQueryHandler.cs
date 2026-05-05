using MediatR;
using AutoMapper;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Features.Users.DTOs;
namespace JobBoard.Application.Features.Users.Queries;
public class GetSearchedUserQueryHandler : IRequestHandler<GetSearchedUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetSearchedUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetSearchedUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(request.SearchTerm, request.Role);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}