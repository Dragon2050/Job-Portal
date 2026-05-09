using JobBoard.Domain.Entities;
using JobBoard.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        public ForgotPasswordCommandHandler(IUserRepository userRepository, IPasswordResetTokenRepository tokenRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }
        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                // For security, we don't reveal whether the email exists or not
                return Unit.Value;
            }

            //Cryptographically secure token generation
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(tokenBytes)
                        .Replace("+", "-")
                        .Replace("/", "_")
                        .Replace("=", "");
            var resetToken = new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };
            await _tokenRepository.AddAsync(resetToken);
            await _emailService.SendPasswordResetEmailAsync(user.Email, user.FullName, token);
            return Unit.Value;
        }
    }
}
