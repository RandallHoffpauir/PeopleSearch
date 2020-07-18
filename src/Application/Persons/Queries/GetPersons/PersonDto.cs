using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleSearch.Application.Persons.Queries.GetPersons
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime BirthDate { get; set; }


    }
}
