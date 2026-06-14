using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if a user with this email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            // 2. Parse the string Role ("Candidate" or "Recruiter") to the Backend Role Enum
            if (!Enum.TryParse<Role>(request.Role, out var roleEnum))
            {
                throw new Exception("Invalid user role specified.");
            }

            // 3. Create the new User entity and hash the password using BCrypt
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = roleEnum
            };

            // 4. Save to the database
            await _userRepository.AddAsync(newUser);

            return newUser.Id;
        }
    }
}
