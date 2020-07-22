using System;
using System.Collections.Generic;
using PeopleSearch.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "jason@clean-architecture", Email = "jason@clean-architecture" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "PeopleSearch!");
            }
        }

        public static async Task SeedPersonsAsync(ApplicationDbContext context)
        {
            var person = new Person()
            {
                FirstName = "John",
                LastName = "Smith",
                Address = "123 Main",
                City = "Lafayette",
                State = "Louisiana",
                Zip = "70503",
                BirthDate = new DateTime(1961, 3, 14),
                Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Cycling"},
                    new PersonInterest(){Interest = "Fishing"}
                }
            };
            await context.AddAsync(person);


            person = new Person()
            {
                FirstName = "Mary",
                LastName = "Allen",
                Address = "345 Oak Blvd",
                City = "Houston",
                State = "Texas",
                Zip = "12345",
                BirthDate = new DateTime(1956, 2, 14),
                Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Hiking"}
                }
            };
            await context.AddAsync(person);

            person = new Person()
            {
                FirstName = "Sally",
                LastName = "Jones",
                Address = "567 Pine Avenue",
                City = "Memphis",
                State = "Tennessee",
                Zip = "38138",
                BirthDate = new DateTime(1971, 12, 1),
            };
            await context.AddAsync(person);

            person = new Person()
            {
                FirstName = "Rick",
                LastName = "Clark",
                Address = "9834 Hwy 20",
                City = "Tampa",
                State = "Florida",
                Zip = "44556",
                BirthDate = new DateTime(1999, 7, 7),
                Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Photography"},
                    new PersonInterest(){Interest = "Reading"},
                    new PersonInterest(){Interest = "Painting"}
                }
            };
            await context.AddAsync(person);


            person = new Person()
            {
                FirstName = "Alice",
                LastName = "Morris",
                Address = "965 Overland Road",
                City = "Greenville",
                State = "South Carolina",
                Zip = "98765",
                BirthDate = new DateTime(2003, 4, 25),
            };
            await context.AddAsync(person);

        }
    }
}
