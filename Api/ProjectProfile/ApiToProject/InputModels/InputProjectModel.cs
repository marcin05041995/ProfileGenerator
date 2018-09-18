using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.InputModels
{
    public class InputProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClientSector { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ProjectEmployeeInputModel
    {
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Podaj specjzalizacje")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Podaj rating")]
        [Range(1, 5, ErrorMessage = "Zakres jest od 1 do 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Podaj ilość lat pracy w zawodzie.")]
        public int YearsOfWork { get; set; }

    }

    public class ProjectSkillInputModel
    {
        public Guid ProjectId { get; set; }
        public Guid SkillId { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        public double ExperienceInYears { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        [Range(1, 5, ErrorMessage = "Oceń w skali od 1 do 5")]
        public int Profficiency { get; set; }
    }

    public class ProjectTechnologyInputModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Guid TechnologyId { get; set; }
        
        public string TechnologiesName { get; set; }

    }
}
