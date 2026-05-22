using AutoMapper;
using JobBoard.Application.Features.Applications.DTOs;

namespace JobBoard.Application.Mappings
{
    public class ApplicationMappingProfile : JobMappingProfile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<JobBoard.Domain.Entities.Application, ApplicationResponseDto>()
                .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CandidateEmail, opt => opt.MapFrom(src => src.User.Email));
            
            CreateMap<JobBoard.Domain.Entities.Application, CandidateApplicationDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title))
                .ForMember(dest => dest.JobLocation, opt => opt.MapFrom(src => src.Job.Location));
        }
    }
}