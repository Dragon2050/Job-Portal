using JobBoard.Domain.Enums;
using System;

namespace JobBoard.Application.Features.Applications.DTOs
{
    public class CandidateApplicationDto
    {
        public Guid Id {get; set;}
        public Guid JobId {get; set;}
        public string JobTitle {get; set;}=string.Empty;
        public string JobLocation {get; set;}=string.Empty;
        public ApplicationStatus Status {get; set;}
        public DateTime AppliedAt {get; set;}
    }
}