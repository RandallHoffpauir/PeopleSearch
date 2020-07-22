using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeopleSearch.Application.Persons.Commands.UpdatePerson;
using PeopleSearch.Application.TodoItems.Commands.UpdateTodoItem;
using Xunit;

namespace PeopleSearch.WebUI.IntegrationTests.Controllers.Persons
{
    public class Update : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Update(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidUpdatePersonCommand_ReturnsSuccessCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new UpdatePersonCommand()
            {
                Id = 1,
                FirstName = "fname",
                LastName = "lname",
                Address = "addr",
                City = "city",
                State = "st",
                Zip = "12345",
                BirthDate = DateTime.Now
            };

            var content = IntegrationTestHelper.GetRequestContent(command);

            var response = await client.PutAsync($"/api/persons/{command.Id}", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
