using System;
using System.Collections.Generic;
using System.IO;
using PeopleSearch.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using PeopleSearch.Domain.Entities;
using System.Drawing;

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

            if (!context.Persons.Any())
            {

                var path = Directory.GetCurrentDirectory() + "\\ClientApp\\src\\assets\\images\\";

                var photo = System.IO.File.ReadAllBytes(path + "bob.jpg");
                var person = new Person()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "123 Main",
                    City = "Lafayette",
                    State = "Louisiana",
                    Zip = "70503",
                    BirthDate = new DateTime(1961, 3, 14),
                    Photo = photo,
                    Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Cycling"},
                    new PersonInterest(){Interest = "Fishing"}
                }
                };
                await context.Persons.AddAsync(person);


                photo = System.IO.File.ReadAllBytes(path + "beth.jpg");
                person = new Person()
                {
                    FirstName = "Mary",
                    LastName = "Allen",
                    Address = "345 Oak Blvd",
                    City = "Houston",
                    State = "Texas",
                    Zip = "12345",
                    BirthDate = new DateTime(1956, 2, 14),
                    Photo = photo,
                    Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Hiking"}
                }
                };
                await context.Persons.AddAsync(person);

                photo = System.IO.File.ReadAllBytes(path + "sally.jpg");
                person = new Person()
                {
                    FirstName = "Sally",
                    LastName = "Jones",
                    Address = "567 Pine Avenue",
                    City = "Memphis",
                    State = "Tennessee",
                    Zip = "38138",
                    Photo = photo,
                    BirthDate = new DateTime(1971, 12, 1),
                };
                await context.Persons.AddAsync(person);

                photo = System.IO.File.ReadAllBytes(path + "dan.jpg");
                person = new Person()
                {
                    FirstName = "Rick",
                    LastName = "Clark",
                    Address = "9834 Hwy 20",
                    City = "Tampa",
                    State = "Florida",
                    Zip = "44556",
                    BirthDate = new DateTime(1999, 7, 7),
                    Photo = photo,
                    Interests = new List<PersonInterest>()
                {
                    new PersonInterest(){Interest = "Photography"},
                    new PersonInterest(){Interest = "Reading"},
                    new PersonInterest(){Interest = "Painting"}
                }
                };
                await context.Persons.AddAsync(person);


                photo = System.IO.File.ReadAllBytes(path + "jim.jpg");
                person = new Person()
                {
                    FirstName = "Jim",
                    LastName = "Morris",
                    Address = "965 Overland Road",
                    City = "Greenville",
                    State = "South Carolina",
                    Zip = "98765",
                    Photo = photo,
                    BirthDate = new DateTime(2003, 4, 25),
                };
                await context.Persons.AddAsync(person);

                await context.SaveChangesAsync();
            }

        }
    }
}
