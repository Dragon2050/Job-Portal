using MediatR;
using JobBoard.Application.Interfaces;
using JobBoard.Application.Interfaces;

namespace JobBoard.Application.Features.Users.Commands
{
    public class UploadCvCommandHandler: IRequestHandler<UploadCvCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        public UploadCvCommandHandler(IUserRepository userRepository, IFileService fileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
        }
        public async Task<string> Handle(UploadCvCommand request, CancellationToken cancellationToken)
        {
            //1. Check if user exists
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            //If user already has a CV, delete the old one to save space
            if(!string.IsNullOrEmpty(user.CVPath))
            {
                _fileService.DeleteFile(user.CVPath);
            }
            //Save the new file using our service
            string savedPath = await _fileService.SaveFileAsync(request.FileData, request.FileName, "cvs");

            //Update the user in the database
            user.CVPath = savedPath;
            await _userRepository.UpdateAsync(user);
            return savedPath;
        }
    }
}
