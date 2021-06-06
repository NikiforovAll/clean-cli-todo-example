namespace CleanCli.Todo.Domain.Entities
{
    using CleanCli.Todo.Domain.Common;
    using CleanCli.Todo.Domain.ValueObjects;
    using System.Collections.Generic;

    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
