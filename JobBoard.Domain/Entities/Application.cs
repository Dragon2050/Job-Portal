using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Entities
{
    public class Application
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public Guid CandidateId { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public string ResumePath { get; set; } = string.Empty;
        //Navigation
        public Job Job { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
