namespace CleanCli.Todo.Application.TodoLists.Queries.GetTodos
{
    using CleanCli.Todo.Application.Common.Mappings;
    using CleanCli.Todo.Domain.Entities;
    using System.Collections.Generic;

    public class TodoListDto : IMapFrom<TodoList>
    {
        public TodoListDto() => this.Items = new List<TodoItemDto>();

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
