using ApiToProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Models
{
    public class SkillViewModel
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public SkillGroup SkillGroup { get; set; }


    }
}
