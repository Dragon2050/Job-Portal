using MediatR;
using JobBoard.Domain.Interfaces;
using JobBoard.Domain.Enums;
using DomainApplication = JobBoard.Domain.Entities.Application;

namespace JobBoard.Application.Features.Applications.Commands
{
    public class UpdateApplicationStatusCommandHandler: IRequestHandler<UpdateApplicationStatusCommand, bool>
    {
        private readonly IApplicationRepository _applicationRepository;
        
        public UpdateApplicationStatusCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<bool> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
            if(application == null) throw new Exception("Application not found");
            // Security Check: Did this recruiter create the job?
            if(application.Job.CreatedById != request.RecruiterId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this application for this job.");
            }
            //Update the Status
            application.Status = request.Status;
            //Save to Database
            await _applicationRepository.UpdateAsync(application);
            return true;
        }        
    }
}