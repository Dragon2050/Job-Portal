using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Application.Interfaces;

namespace JobBoard.Application.Features.Auth.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordResetTokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // Find the token in DB
            var resetToken = await _tokenRepository.GetByTokenAsync(request.Token);
            if(resetToken == null)
            {
                throw new Exception("Invalid or expired token");
            }
            if(resetToken.IsUsed)
            {
                throw new Exception("Token has already been used");
            }
            if(resetToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("Token has expired");
            }

            // Validate new password and confirm password
            if(request.NewPassword != request.ConfirmPassword)
            {
                throw new Exception("Password do not match");
            }
            if(request.NewPassword.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long");
            }

            // Get the user and update the password
            var user = await _userRepository.GetByIdAsync(resetToken.UserId);
            if(user == null)
                throw new Exception("User not found");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userRepository.UpdateAsync(user);

            //Hard delete the token after use
            await _tokenRepository.DeleteAsync(resetToken);
            return Unit.Value;
        }
    }
}
