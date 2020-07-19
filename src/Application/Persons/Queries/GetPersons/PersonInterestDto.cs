using System;
using System.Collections.Generic;
using System.Text;
using PeopleSearch.Application.Common.Mappings;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Application.Persons.Queries.GetPersons
{
    public class PersonInterestDto : IMapFrom<PersonInterest>
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Interest { get; set; }
    }
}
