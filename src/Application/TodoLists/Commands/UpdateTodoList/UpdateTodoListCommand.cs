namespace CleanCli.Todo.Application.TodoLists.Commands.UpdateTodoList
{
    using CleanCli.Todo.Application.Common.Exceptions;
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateTodoListCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TodoLists.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            entity.Title = request.Title;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
