using System;
using System.Collections.Generic;
using System.Text;
using PeopleSearch.Domain.Common;

namespace PeopleSearch.Domain.Entities
{
    public class PersonInterest : AuditableEntity
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Interest { get; set; }

        public Person Person { get; set; }
    }
}
