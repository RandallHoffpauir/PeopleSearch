﻿using PeopleSearch.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace PeopleSearch.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
