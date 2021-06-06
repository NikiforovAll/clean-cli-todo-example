namespace CleanCli.Todo.Domain.Events
{
    using CleanCli.Todo.Domain.Common;
    using CleanCli.Todo.Domain.Entities;

    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item) => this.Item = item;

        public TodoItem Item { get; }
    }
}
