using ApiToProject.Entities;
using ApiToProject.InputModels;
using ApiToProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiToProject.Controllers
{
    [Route("api/skills")]
    public class SkillController:Controller
    {
        private readonly DataBaseContext context;

        public SkillController(DataBaseContext context)
        {
            this.context = context;
        }
        

        [HttpPost]
        [Route("AddSkill")]
        public IActionResult AddSkill(InputSkillModel inputSkillModel)
        {

            context.Skills.Add(new Skill()
            {
                SkillName = inputSkillModel.Name,
            });
            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("GetSkill")]
        public IActionResult GetSkill(Guid id)
        {
            if (id == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            var skill = context.Skills.FirstOrDefault(x => x.Id == id);

            if (skill == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            return StatusCode((int)HttpStatusCode.OK, skill);
        }

        [HttpGet]
        [Route("GetSkills")]
        public IActionResult GetSkills()
        {
            //var skill = context.Skills?.ToList();

           //if (skill == null || !skill.Any())
               //return new StatusCodeResult((int)HttpStatusCode.NotFound);

            var tmp = context.Skills
                 .ToList();

            var output = new List<SkillViewModel>();

            foreach (var skil in tmp)
            {
                output.Add(new SkillViewModel
                {
                    Id = skil.Id,
                    SkillName = skil.SkillName,
                    SkillGroup = skil.SkillGroup
                });
            }

            return StatusCode((int)HttpStatusCode.OK, output);

        }
        
        [HttpPut]
        [Route("EditSkill")]
        public IActionResult EditSkill(InputSkillModel inputSkillModel)
        {
            if (inputSkillModel == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var skill = context.Skills.FirstOrDefault(x => x.Id == inputSkillModel.Id);

            if (skill == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            skill.SkillName = inputSkillModel.Name;
            context.SaveChanges();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("DeleteSkill")]
        public IActionResult DeleteSkill(Guid Id)
        {
            Skill skill = context.Skills.Find(Id);
            context.Skills.Remove(skill);

            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
