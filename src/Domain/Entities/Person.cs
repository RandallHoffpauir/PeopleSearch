using System;
using System.Collections.Generic;
using System.Text;
using PeopleSearch.Domain.Common;

namespace PeopleSearch.Domain.Entities
{
    public class Person : AuditableEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime BirthDate { get; set; }

        public byte[] Photo { get; set; }
    }
}
