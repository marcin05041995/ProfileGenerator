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
    [Route("api/languages")]
    public class LanguageController : Controller
    {

        private readonly DataBaseContext context;

        public LanguageController(DataBaseContext context)
        {
            this.context = context;
        }


        [HttpPost]
        [Route("AddLanguage")]
        public IActionResult AddLanguage(InputLanguageModel inputLanguageModel)
        {

            context.Languages.Add(new Language()
            {
                LanguageName = inputLanguageModel.Name
            });
            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("GetLanguage")]
        public IActionResult GetLanguage(Guid id)
        {
            if (id == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var language = context.Languages.FirstOrDefault(x => x.Id == id);

            if (language == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            return StatusCode((int)HttpStatusCode.OK, language);
        }

        [HttpGet]
        [Route("GetLanguages")]
        public IActionResult GetLanguages()
        {
            var language = context.Languages?.ToList();

            if (language == null || !language.Any())
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            return StatusCode((int)HttpStatusCode.OK, language);
        }

        [HttpPut]
        [Route("EditLanguage")]
        public IActionResult EditLanguage(InputLanguageModel inputLanguageModel)
        {
            if (inputLanguageModel == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            var language = context.Languages.FirstOrDefault(x => x.Id == inputLanguageModel.Id);

            if (language == null)
                return new StatusCodeResult((int)HttpStatusCode.NotFound);

            language.LanguageName = inputLanguageModel.Name;         
            context.SaveChanges();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("DeleteLanguages")]
        public IActionResult DeleteLanguages(Guid Id)
        {
            Language language = context.Languages.Find(Id);
            context.Languages.Remove(language);

            context.SaveChanges();
            return StatusCode((int)HttpStatusCode.OK);
        }
    }    
}
