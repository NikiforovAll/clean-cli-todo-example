namespace CleanCli.Todo.Application.TodoItems.EventHandlers
{
    using CleanCli.Todo.Application.Common.Models;
    using CleanCli.Todo.Domain.Events;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class TodoItemCreatedHandler : INotificationHandler<DomainEventNotification<TodoItemCreatedEvent>>
    {
        private readonly ILogger<TodoItemCompletedHandler> logger;

        public TodoItemCreatedHandler(ILogger<TodoItemCompletedHandler> logger) => this.logger = logger;

        public Task Handle(DomainEventNotification<TodoItemCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            this.logger.LogInformation("CleanArchitectureJT.Example Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
