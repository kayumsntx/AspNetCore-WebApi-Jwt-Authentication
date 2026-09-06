using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }= null!;
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Experience>Experiences { get; set; } = new List<Experience>();

    }
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string ExperienceTitle { get; set; } = null!;
        public int  Duration { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
