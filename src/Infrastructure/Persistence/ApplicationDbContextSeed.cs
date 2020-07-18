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
                Zip = "70503"
            };
            await context.Persons.AddAsync(person);


        }
    }
}
