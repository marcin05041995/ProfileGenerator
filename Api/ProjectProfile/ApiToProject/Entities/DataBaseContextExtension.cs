using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<DataBaseContext>();
            context.Database.EnsureCreated();
            if (!context.Employees.Any())
            {

                try
                {
                    var employees = new List<Employee>()
                {
                new Employee(){ FirstName ="Jan", LastName ="Kowalski", Specialization =".Net Developer",Rating =3,YearsOfWork =2 },
                new Employee(){FirstName="Sebastian",LastName="Nowak",Specialization=".Net Developer", Rating=2, YearsOfWork=1},
                new Employee(){FirstName="Wojtek",LastName="Sarski",Specialization=".Net Developer", Rating=4, YearsOfWork=3}
                };

                    var projects = new List<Project>()
                {
                    new Project(){Name="Project",Title="Linora-Application for managing animal transport" , ClientSector="Transport animal", StartDate=new DateTime(2017,11,21), EndDate=new DateTime(2017,12,14)},
                    new Project(){Name="Project1",Title="Be-there- Appliacation for managing events and ticket brokering" , ClientSector="Services", StartDate=new DateTime(2018,01,10), EndDate=new DateTime(2018,02,04)},
                    new Project(){Name="Project2",Title="Ecologistics" , ClientSector="Spedition", StartDate=new DateTime(2018,02,10), EndDate=new DateTime(2018,03,15)}
                };

                var languages = new List<Language>()
                {
                    new Language(){LanguageName="English"},
                    new Language(){LanguageName="Polish"},
                    new Language(){LanguageName="Spanish"},
                    new Language(){LanguageName="German"},
                    new Language(){LanguageName="French"},
                    new Language(){LanguageName="Italian"}
                };

                 var skills = new List<Skill>()
                {
                    new Skill() {SkillName="Windows Forms", SkillGroup=SkillGroup.Frontend}, 
                    new Skill() {SkillName="C#", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName="UML", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName="Entity Framework", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName="ASP.Net", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName="ASP.Net MVC 2/3/4/5", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName=".Net Framework 2/3/4/5", SkillGroup=SkillGroup.Backend},
                    new Skill() {SkillName="JS/Jquery", SkillGroup=SkillGroup.Frontend},
                    new Skill() {SkillName="T-SQL", SkillGroup=SkillGroup.DB},
                    new Skill() {SkillName="MS SQL 2008/2012/2015", SkillGroup=SkillGroup.DB},
                    new Skill() {SkillName="Mongo DB", SkillGroup=SkillGroup.DB},
                    new Skill() {SkillName="PostgreSQL", SkillGroup=SkillGroup.DB},
                    new Skill() {SkillName="HTML/CSS/Bootstrap", SkillGroup=SkillGroup.Frontend},
                    new Skill() {SkillName="Kendo UI", SkillGroup=SkillGroup.Frontend},
                    new Skill() {SkillName="AJAX", SkillGroup=SkillGroup.Frontend},
                    new Skill() {SkillName="Linux", SkillGroup=SkillGroup.Admin},
                    new Skill() {SkillName="OpenStreet Maps", SkillGroup=SkillGroup.Frontend},
                    new Skill() {SkillName="Windows Phone", SkillGroup=SkillGroup.Mobile},

                };
                    var technologies = new List<Technology>()
                {
                    new Technology() {TechnologyName="Windows Forms"},
                    new Technology() {TechnologyName="C#"},
                    new Technology() {TechnologyName="UML"},
                    new Technology() {TechnologyName="Entity Framework"},
                    new Technology() {TechnologyName="ASP.Net"},
                    new Technology() {TechnologyName="ASP.Net MVC 2/3/4/5"},
                    new Technology() {TechnologyName=".Net Framework 2/3/4/5"},
                    new Technology() {TechnologyName="JS/Jquery"},
                    new Technology() {TechnologyName="T-SQL"},
                    new Technology() {TechnologyName="MS SQL 2008/2012/2015"},
                    new Technology() {TechnologyName="Mongo DB"},
                    new Technology() {TechnologyName="PostgreSQL"},
                    new Technology() {TechnologyName="HTML/CSS/Bootstrap"},
                    new Technology() {TechnologyName="Kendo UI"},
                    new Technology() {TechnologyName="AJAX"},
                    new Technology() {TechnologyName="Linux"},
                    new Technology() {TechnologyName="OpenStreet Maps"},
                    new Technology() {TechnologyName="Windows Phone"},
                    new Technology() {TechnologyName="Entity Framework Code First" }

                };


                context.Employees.AddRange(employees);
                context.Projects.AddRange(projects);
                context.Languages.AddRange(languages);
                context.Skills.AddRange(skills);
                context.Technologies.AddRange(technologies);
                    context.SaveChanges();

                 var employeeproject = new List<EmployeeProject>() {
                 new EmployeeProject { EmployeeId=employees[0].Id,ProjectId=projects[0].Id},
                 new EmployeeProject { EmployeeId=employees[0].Id,ProjectId=projects[1].Id}     
                };

                var employeeskill = new List<EmployeeSkill>()
                {
                 new EmployeeSkill { EmployeeId=employees[0].Id,SkillId=skills[0].Id},
                 new EmployeeSkill { EmployeeId=employees[0].Id,SkillId=skills[1].Id}
                };

                var employeelanguage = new List<EmployeeLanguage>()
                {
                 new EmployeeLanguage { EmployeeId=employees[0].Id,LanguageId=languages[0].Id},
                 new EmployeeLanguage { EmployeeId=employees[0].Id,LanguageId=languages[1].Id}
                };

                var projecttechnology = new List<ProjectTechnology>()
                {
                 new ProjectTechnology { ProjectId=projects[0].Id,TechnologyId=technologies[0].Id},
                 new ProjectTechnology { ProjectId=projects[0].Id,TechnologyId=technologies[1].Id}
                };
                    context.EmployeeProjects.AddRange(employeeproject);
                    context.EmployeeSkills.AddRange(employeeskill);
                    context.EmployeeLanguages.AddRange(employeelanguage);
                    context.ProjectTechnologies.AddRange(projecttechnology);

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                }

            }
        }
    }
}
