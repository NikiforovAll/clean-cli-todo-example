namespace CleanCli.Todo.Application.TodoItems.Commands.UpdateTodoItem
{
    using CleanCli.Todo.Application.Common.Exceptions;
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateTodoItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TodoItems.FindAsync(
                new object[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
