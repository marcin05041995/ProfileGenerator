using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClientSector { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsArchive { get; set; }

        public IList<EmployeeViewModel> Profiles { get; set; }
        public IList<ProjectTechnologyViewModel> Technology { get; set; }
    }

  //  public class ProjectProfileViewModel
  //  {
   //     public Guid Id { get; set; }
   //     public string Name { get; set; }
   //     public string Surname { get; set; }
   // }

    public class ProjectTechnologyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
