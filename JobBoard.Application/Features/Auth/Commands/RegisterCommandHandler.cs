using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using JobBoard.Domain.Interfaces;
using MediatR;
using BCrypt.Net;

namespace JobBoard.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand,Guid>
    {
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing != null) 
                throw new Exception("User already exists");

            if (!Enum.TryParse<Role>(request.Role, true, out var role))
                throw new Exception("Invalid role");

            if (role == Role.Admin)
                throw new Exception("Admin not allowed");
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
