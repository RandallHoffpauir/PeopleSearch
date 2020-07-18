using PeopleSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace PeopleSearch.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Person> Persons { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
