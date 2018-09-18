using ApiToProject.Entities;
using ApiToProject.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiToProject.Controllers
{
    [Route("api/technologies")]

    public class TechnologyController : Controller
    {
        private readonly DataBaseContext context;

        public TechnologyController(DataBaseContext context)
        {
            this.context = context;
        }


        [HttpPost]
        [Route("AddTechnology")]
        public IActionResult AddTechnology(InputTechnologyModel inputTechnologyModel)
        {

            context.Technologies.Add(new Technology()
            {
                TechnologyName = inputTechnologyModel.Name
            });
            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("GetTechnology")]
        public IActionResult GetTechnology(Guid id)
        {
            if (id == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var technology = context.Technologies.FirstOrDefault(x => x.Id == id);

            if (technology == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            return StatusCode((int)HttpStatusCode.OK, technology);
        }

        [HttpGet]
        [Route("GetTechnologies")]
        public IActionResult GetTechnologies()
        {
            var technology = context.Technologies?.ToList();

            if (technology == null || !technology.Any())
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            return StatusCode((int)HttpStatusCode.OK, technology);
        }

        [HttpPut]
        [Route("EditTechnology")]
        public IActionResult EditTechnology(InputTechnologyModel inputTechnologyModel)
        {
            if (inputTechnologyModel == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var technology = context.Technologies.FirstOrDefault(x => x.Id == inputTechnologyModel.Id);

            if (technology == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            technology.TechnologyName = inputTechnologyModel.Name;
            context.SaveChanges();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("DeleteTechnology")]
        public IActionResult DeleteTechnology(Guid Id)
        {
            Technology technology = context.Technologies.Find(Id);
            context.Technologies.Remove(technology);

            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }
    }


}

