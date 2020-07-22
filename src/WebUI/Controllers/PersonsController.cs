using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleSearch.Application.Persons.Commands.CreatePerson;
using PeopleSearch.Application.Persons.Commands.UpdatePerson;
using PeopleSearch.Application.Persons.Queries.GetPersons;

namespace PeopleSearch.WebUI.Controllers
{
    [Authorize]
    public class PersonsController : ApiController
    {

        [HttpGet]
        public async Task<ActionResult<PersonsVm>> Get(string nameSearch)
        {
            // slowing down the process on purpose
            Thread.Sleep(3000);

            return await Mediator.Send(new GetPersonsQuery()
            {
                NameSearch = nameSearch
            });
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreatePersonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdatePersonCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [Route("{id}/photo")]
        [HttpPost]
        public async Task<ActionResult<long>> UploadImage([FromRoute] long id)
        {
            var file = Request.Form.Files[0];

            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);

                await Mediator.Send(new UpdatePersonPhotoCommand()
                {
                    PersonId = id,
                    Photo = ms.ToArray()
                });
            }

            return Ok(new { Status = "File Upload successful." });
        }

        [Route("{id}/photo")]
        [HttpGet]
        public async Task<ActionResult<string>> GetImage([FromRoute] long id)
        {
            try
            {
                var photo = await Mediator.Send(new GetPersonPhotoQuery()
                {
                    PersonId = id
                });

                
                return photo;
                //return File(photo, "image/jpeg");

            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
                throw;
            }

        }

    }

    public class CreatePhoto
    {
        public string PhotoName { get; set; }
        public IFormFile PhotoFile { get; set; }
    }

}
