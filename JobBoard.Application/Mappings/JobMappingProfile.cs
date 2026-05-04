using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobBoard.Domain.Entities;
using JobBoard.Application.Features.Jobs.DTOs;

namespace JobBoard.Application.Mappings
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Job, JobResponseDto>();
            CreateMap<Job, RecruiterJobDto>();
        }
    }
}
