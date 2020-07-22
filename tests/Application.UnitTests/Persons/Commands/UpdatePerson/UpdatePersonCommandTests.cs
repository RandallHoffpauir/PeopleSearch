using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeopleSearch.Application.Common.Exceptions;
using PeopleSearch.Application.Persons.Commands.UpdatePerson;
using PeopleSearch.Application.UnitTests.Common;
using Shouldly;
using Xunit;

namespace PeopleSearch.Application.UnitTests.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldPersistPerson()
        {

            // Arrange

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

            var handler = new UpdatePersonCommand.UpdatePersonCommandHandler(Context);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var entity = Context.Persons.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.FirstName.ShouldBe(command.FirstName);
            entity.LastName.ShouldBe(command.LastName);
            entity.Address.ShouldBe(command.Address);
            entity.City.ShouldBe(command.City);
            entity.State.ShouldBe(command.State);
            entity.Zip.ShouldBe(command.Zip);
            entity.BirthDate.ShouldBe(command.BirthDate);

        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
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

            var sut = new UpdatePersonCommand.UpdatePersonCommandHandler(Context);

            // Act
            // Assert
            Should.ThrowAsync<NotFoundException>(() =>
                sut.Handle(command, CancellationToken.None));
        }
    }
}
