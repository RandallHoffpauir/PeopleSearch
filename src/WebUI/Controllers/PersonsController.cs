using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleSearch.Application.Persons.Queries.GetPersons;

namespace PeopleSearch.WebUI.Controllers
{
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
    }
}
