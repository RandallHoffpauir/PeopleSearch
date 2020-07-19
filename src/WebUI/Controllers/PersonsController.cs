using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    }
}
