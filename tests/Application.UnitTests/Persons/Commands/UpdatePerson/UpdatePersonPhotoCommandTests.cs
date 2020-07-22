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
    public class UpdatePersonPhotoCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldPersistPhoto()
        {
            // Arrange

            var command = new UpdatePersonPhotoCommand()
            {
                PersonId = 1,
                Photo = Encoding.ASCII.GetBytes("somestring")
            };

            var handler = new UpdatePersonPhotoCommand.UpdatePersonPhotoCommandHandler(Context);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var entity = Context.Persons.Find(command.PersonId);

            entity.ShouldNotBeNull();

            entity.Photo.ShouldNotBeNull();
        }


        [Fact]
        public void Handle_GivenInvalidId_ShouldThrowException()
        {
            // Arrange

            var command = new UpdatePersonPhotoCommand()
            {
                PersonId = 999,
                Photo = Encoding.ASCII.GetBytes("somestring")
            };

            var sut = new UpdatePersonPhotoCommand.UpdatePersonPhotoCommandHandler(Context);

            // Act
            // Assert
            Should.ThrowAsync<NotFoundException>(() =>
                sut.Handle(command, CancellationToken.None));
        }

    }
}
