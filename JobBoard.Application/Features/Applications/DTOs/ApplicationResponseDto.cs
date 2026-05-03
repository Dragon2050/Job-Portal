using JobBoard.Domain.Enums;
using System;

namespace JobBoard.Application.Features.Applications.DTOs
{
    public class ApplicationResponseDto
    {
        public Guid Id {get; set;}
        public Guid JobId {get; set;}
        public Guid CandidateId {get; set;}
        public string CandidateName {get; set;} = "";
        public string CandidateEmail {get; set;} = "";
        public ApplicationStatus Status {get; set;}
        public DateTime AppliedAt {get; set;}
    }
}