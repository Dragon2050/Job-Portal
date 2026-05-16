using MediatR;

namespace JobBoard.Application.Features.Users.Commands
{
    public class UploadCvCommand : IRequest<string>
    {
        public Guid UserId {get; set;}
        public byte[] FileData {get; set;} = Array.Empty<byte>();
        public string FileName {get; set;} = string.Empty;
    }
}