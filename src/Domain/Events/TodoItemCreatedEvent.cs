namespace CleanCli.Todo.Domain.Events
{
    using CleanCli.Todo.Domain.Common;
    using CleanCli.Todo.Domain.Entities;

    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item) => this.Item = item;

        public TodoItem Item { get; }
    }
}
