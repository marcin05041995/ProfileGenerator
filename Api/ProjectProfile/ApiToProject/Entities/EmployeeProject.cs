using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class EmployeeProject
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }

        public DateTime JoinDate { get; set; }




    }
}
