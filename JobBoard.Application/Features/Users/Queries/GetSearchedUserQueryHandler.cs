using MediatR;
using AutoMapper;
using JobBoard.Domain.Interfaces;
using JobBoard.Application.Features.Users.DTOs;
using JobBoard.Application.Common;
namespace JobBoard.Application.Features.Users.Queries;
public class GetSearchedUserQueryHandler : IRequestHandler<GetSearchedUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetSearchedUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<UserDto>> Handle(GetSearchedUsersQuery request, CancellationToken cancellationToken)
    {
        var (users, totalCount) = await _userRepository.GetAllAsync(request.SearchTerm, request.Role, request.PageNumber, request.PageSize);
        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return new PagedResult<UserDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}