using PeopleSearch.Application.Common.Interfaces;
using System;

namespace PeopleSearch.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
