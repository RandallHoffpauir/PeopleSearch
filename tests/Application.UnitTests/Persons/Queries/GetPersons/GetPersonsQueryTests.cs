using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class GetPersonsQueryTests
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetPersonsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            // Arrange
            var query = new GetPersonsQuery()
            {
                NameSearch = "name"
            };

            var handler = new GetPersonsQuery.GetPersonQueryHandler(_context, _mapper);

            
            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PersonsVm>();
            result.Persons.Count.ShouldBe(1);

            var person = result.Persons.First();

            person.Interests.Count.ShouldBe(4);
        }

    }
}
