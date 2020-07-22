using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeopleSearch.Application.Persons.Commands.CreatePerson;
using PeopleSearch.Application.UnitTests.Common;
using PeopleSearch.Infrastructure.Services;
using Shouldly;
using Xunit;

namespace PeopleSearch.Application.UnitTests.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_ShouldPeristPerson()
        {

            // Arrange
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

            var handler = new CreatePersonCommand.CreatePersonCommandHandler(Context);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var entity = Context.Persons.Find(result);

            entity.ShouldNotBeNull();
            entity.FirstName.ShouldBe(command.FirstName);
            entity.LastName.ShouldBe(command.LastName);
            entity.Address.ShouldBe(command.Address);
            entity.City.ShouldBe(command.City);
            entity.State.ShouldBe(command.State);
            entity.Zip.ShouldBe(command.Zip);
            entity.BirthDate.ShouldBe(command.BirthDate);
        }
    }
}
