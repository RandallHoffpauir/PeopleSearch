using PeopleSearch.Application.Common.Interfaces;
using System;

namespace PeopleSearch.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
