using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PeopleSearch.Application.Persons.Queries.GetPersons;
using PeopleSearch.Application.UnitTests.Common;
using PeopleSearch.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace PeopleSearch.Application.UnitTests.Persons.Queries.GetPersons
{
    [Collection("QueryTests")]
    public class GetPersonPhotoQueryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetPersonPhotoQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectString()
        {
            // Arrange
            var query = new GetPersonPhotoQuery()
            {
                PersonId = 1
            };

            var handler = new GetPersonPhotoQuery.GetPersonPhotoQueryHandler(_context);


            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<string>();

            result.ShouldStartWith("data:image/jpg;base64,");

            
        }

    }
}
