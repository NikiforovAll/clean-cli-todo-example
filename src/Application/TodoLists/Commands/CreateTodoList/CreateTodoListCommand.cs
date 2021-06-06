namespace CleanCli.Todo.Application.TodoLists.Commands.CreateTodoList
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateTodoListCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            this.context.TodoLists.Add(entity);

            await this.context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
