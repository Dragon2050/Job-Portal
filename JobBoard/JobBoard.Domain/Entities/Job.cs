using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Domain.Entities
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location {  get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime? ApplicationDeadline { get; set; }
        public Guid CreatedById { get; set; }

        //Navigation
        public User CreatedBy { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
