using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class Technology
    {
        public Guid Id { get; set; }

        public string TechnologyName { get; set; }

        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }

    }
}
