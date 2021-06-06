namespace CleanCli.Todo.Application.TodoLists.Commands.PurgeTodoLists
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class PurgeTodoListsCommand : IRequest
    {
    }

    public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
    {
        private readonly IApplicationDbContext context;

        public PurgeTodoListsCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
        {
            this.context.TodoLists.RemoveRange(this.context.TodoLists);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
