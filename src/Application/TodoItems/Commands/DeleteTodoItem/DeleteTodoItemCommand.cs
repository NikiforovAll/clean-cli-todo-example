namespace CleanCli.Todo.Application.TodoItems.Commands.DeleteTodoItem
{
    using CleanCli.Todo.Application.Common.Exceptions;
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TodoItems
                .FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            this.context.TodoItems.Remove(entity);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
