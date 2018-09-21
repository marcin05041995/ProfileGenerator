using ApiToProject.Entities;
using ApiToProject.InputModels;
using ApiToProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiToProject.Controllers
{
    [Route("api/projects")]
    public class ProjectController:Controller
    {
        private readonly DataBaseContext context;

        public ProjectController(DataBaseContext context)
        {
            this.context = context;
        }

        private IList<Profile> GenerateProfile(Guid Id)
        {
            var employee = context.Projects.Include(x => x.EmployeeProjects).ThenInclude(x => x.Employee).FirstOrDefault(x => x.Id == Id);
            if (employee == null)
                return null;
            var employees = employee.EmployeeProjects;
            var output = new List<Profile>();
            foreach (var emps in employees)
            {
                output.Add(new Profile
                {
                    Id = emps.Employee.Id,
                    Name = emps.Employee.FirstName,
                    LastName = emps.Employee.LastName,

                });
            }
            return output;
        }


        private IList<ProfileTechnology> GenerateTechnologies(Guid Id)
        {
            var employee = context.Projects.Include(x => x.ProjectTechnology).ThenInclude(x => x.Technology).FirstOrDefault(x => x.Id == Id);
            if (employee == null)
                return null;
            var technologies = employee.ProjectTechnology;
            var output = new List<ProfileTechnology>();

            foreach (var techno in technologies)
            {
                output.Add(new ProfileTechnology
                {
                    Id = techno.Technology.Id,
                    Name = techno.Technology.TechnologyName,
                    
                });
            }

            return output;
        }

        [Route("GetProject")]
        [HttpGet]
        public IActionResult GetProject(Guid Id)
        {
            var project = context.Projects.Include(x => x.EmployeeProjects).ThenInclude(z => z.Employee).FirstOrDefault(x => x.Id == Id);

            var emps = new List<EmployeeViewModel>();
            foreach(var emp in project.EmployeeProjects)
            {
                emps.Add(new EmployeeViewModel
                {
                    Id = emp.Employee.Id,
                    Name = emp.Employee.FirstName,
                    LastName = emp.Employee.LastName
                });
            }
            var projects = context.Projects.Include(x => x.ProjectTechnology).ThenInclude(z => z.Technology).FirstOrDefault(x => x.Id == Id);

            var techno = new List<ProjectTechnologyViewModel>();
            foreach (var tech in project.ProjectTechnology)
            {
                techno.Add(new ProjectTechnologyViewModel
                {
                    Id = tech.TechnologyId,
                    Name = tech.Technology.TechnologyName,
                });
            }
            var output = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Title = project.Title,
                Description = project.Description,
                ClientSector = project.ClientSector,
                StartDate =project.StartDate,
                EndDate = project.EndDate,
                Profiles = emps,
                Technology = techno
                
            };
            return StatusCode((int)HttpStatusCode.OK, output);
        }

        [Route("GetProjects")]
        [HttpGet]
        public IActionResult GetProjects()
        {
            var project = context.Projects.Include(x => x.EmployeeProjects).ThenInclude(z => z.Employee).Include(z => z.ProjectTechnology).ThenInclude(z=> z.Technology).ToList();
            //var project = context.Projects.Include(x => x.EmployeeProjects).ThenInclude(z => z.Employee).ToList();
            var output = new List<ProjectViewModel>();
           
           
            foreach(var proj in project)
            {
                var technologies = new List<ProjectTechnologyViewModel>();
                var employees = new List<EmployeeViewModel>();

                foreach(var emps in proj.EmployeeProjects)
                {
                    employees.Add(new EmployeeViewModel
                    {
                        Id = emps.EmployeeId,
                        Name = emps.Employee.FirstName,
                        LastName = emps.Employee.LastName

                    });
                }

                foreach (var tech in proj.ProjectTechnology)
                {
                    technologies.Add(new ProjectTechnologyViewModel
                    {
                        Id = tech.TechnologyId,
                        Name = tech.Technology.TechnologyName
                    });
                }

                output.Add(new ProjectViewModel
                {
                    Id = proj.Id,
                    Name = proj.Name,
                    Title = proj.Title,
                    Description = proj.Description,
                    ClientSector = proj.ClientSector,
                    StartDate = proj.StartDate,
                    EndDate = proj.EndDate,
                    IsArchive = proj.IsArchive,
                    Technology = technologies,
                    Profiles = employees
                });
            }

            return StatusCode((int)HttpStatusCode.OK, output);
        }

        [HttpPost]
        [Route("AddProject")]
        public IActionResult AddProject([FromBody]InputProjectModel inputProjectModel)
        {
            var xx = ModelState.IsValid;

            context.Projects.Add(new Project()
            {
                Name = inputProjectModel.Name,
                Title=inputProjectModel.Title,
                ClientSector = inputProjectModel.ClientSector,
                StartDate = inputProjectModel.StartDate,
                EndDate = inputProjectModel.EndDate,
            });
            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("EditProject")]
        public IActionResult EditProject(InputProjectModel inputProjectModel)
        {
            if (inputProjectModel == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var project = context.Projects.FirstOrDefault(x => x.Id == inputProjectModel.Id);

            if (project == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            //project.Title = inputProjectModel.Name;
            project.Name = inputProjectModel.Name;
            project.Title = inputProjectModel.Title;
            project.ClientSector = inputProjectModel.ClientSector;
            project.StartDate = inputProjectModel.StartDate;
            project.EndDate = inputProjectModel.EndDate;

            context.SaveChanges();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public IActionResult DeleteProject(Guid Id)
        {
            Project project = context.Projects.Find(Id);
            //context.Projects.Remove(project);
            project.IsArchive = true;

            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("AddEmployeeToProject")]
        public async Task<IActionResult> AddEmployeeToProject([FromBody] ProjectEmployeeInputModel model)
        {
            var project = context.Projects.Include(x => x.EmployeeProjects).FirstOrDefault(x => x.Id == model.ProjectId);
            if (project == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            var emplo = context.Employees.Include(x => x.EmployeeProjects).FirstOrDefault(x => x.Id == model.EmployeeId);
            if (emplo == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            if (project.EmployeeProjects == null)
                project.EmployeeProjects = new List<EmployeeProject>();
            if (emplo.EmployeeProjects == null)
                emplo.EmployeeProjects = new List<EmployeeProject>();

            if (project.EmployeeProjects.Any(x => x.EmployeeId == emplo.Id))
                return StatusCode((int)HttpStatusCode.BadRequest);

            var projectEmplo = new EmployeeProject
            {
                Project = project,
                ProjectId = model.ProjectId,
                Employee = emplo,
                EmployeeId = model.EmployeeId,
                JoinDate = DateTime.Now

            };
            emplo.EmployeeProjects.Add(projectEmplo);
            project.EmployeeProjects.Add(projectEmplo);
            await context.SaveChangesAsync();
            return StatusCode((int)HttpStatusCode.OK);
            
        }
        [HttpPost]
        [Route("AddTechnologyToProject")]
        public async Task<IActionResult> AddTechnologyToProject([FromBody] ProjectTechnologyInputModel model)
        {
            var project = context.Projects.Include(x => x.ProjectTechnology).FirstOrDefault(x => x.Id == model.ProjectId);
            if (project == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            var techno = context.Technologies.Include(x => x.ProjectTechnology).FirstOrDefault(x => x.Id == model.TechnologyId);
            if (techno == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            if (project.ProjectTechnology == null)
                project.ProjectTechnology = new List<ProjectTechnology>();
            if (techno.ProjectTechnology == null)
                techno.ProjectTechnology = new List<ProjectTechnology>();

            if (project.EmployeeProjects.Any(x => x.EmployeeId == techno.Id))
                return StatusCode((int)HttpStatusCode.BadRequest);

            var projectTechno = new ProjectTechnology
            {
                Project = project,
                ProjectId = model.ProjectId,
                Technology = techno,
                TechnologyId = model.TechnologyId,

            };
            techno.ProjectTechnology.Add(projectTechno);
            project.ProjectTechnology.Add(projectTechno);
            await context.SaveChangesAsync();
            return StatusCode((int)HttpStatusCode.OK);

        }


    }
}
