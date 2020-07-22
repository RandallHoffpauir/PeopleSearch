using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PeopleSearch.Application.Persons.Commands.CreatePerson;
using Shouldly;
using Xunit;

namespace PeopleSearch.WebUI.IntegrationTests.Controllers.Persons
{
    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidCreatePersonCommand_ReturnsSuccessCode()
        {
            // Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreatePersonCommand()
            {
                FirstName = "fname",
                LastName = "lname",
                Address = "addr",
                City = "city",
                State = "st",
                Zip = "12345",
                BirthDate = DateTime.Now
            };

            var context = IntegrationTestHelper.GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/persons", context);

            // Assert
            response.EnsureSuccessStatusCode();

        }


    }
}
