using PeopleSearch.Application.Common.Mappings;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
