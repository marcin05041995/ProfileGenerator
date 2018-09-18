using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.InputModels
{
    public class EmployeeInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public int Rating { get; set; }
        public string OverallTenure { get; set; }        
    }

    public class EmployeeLanguageInputModel
    {
        public Guid EmployeeId { get; set; }
        public Guid LanguageId { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole.")]
        [Range(1, 3, ErrorMessage = "Oceń w skali od 1 do 3")]
        public int SpeakingLevel { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole.")]
        [Range(1, 3, ErrorMessage = "Oceń w skali od 1 do 3")]
        public int WritingLevel { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole.")]
        [Range(1, 3, ErrorMessage = "Oceń w skali od 1 do 3")]
        public int ReadingLevel { get; set; }
    }

    public class EmployeeSkillInputModel
    {
        public Guid EmployeeId { get; set; }
        public Guid SkillId { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        public double ExperienceInYears { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        [Range(1, 5, ErrorMessage = "Oceń w skali od 1 do 5")]
        public int Profficiency { get; set; }
    }

    public class EmployeeProjectInputModel
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole: ")]
        public string ClientSector { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole: ")]
        public string Technologies { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
