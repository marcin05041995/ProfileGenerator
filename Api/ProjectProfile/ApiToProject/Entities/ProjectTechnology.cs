using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class ProjectTechnology
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Guid TechnologyId { get; set; }

        public Project Project { get; set; }
        public Technology Technology { get; set; }



    }
}
