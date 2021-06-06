namespace CleanCli.Todo.Application.TodoItems.Commands.CreateTodoItem
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using CleanCli.Todo.Domain.Events;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                ListId = request.ListId,
                Title = request.Title,
                Done = false
            };

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            this.context.TodoItems.Add(entity);

            await this.context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
