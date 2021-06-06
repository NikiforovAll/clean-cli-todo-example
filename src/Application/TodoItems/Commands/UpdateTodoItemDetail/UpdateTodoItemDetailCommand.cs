namespace CleanCli.Todo.Application.TodoItems.Commands.UpdateTodoItemDetail
{
    using CleanCli.Todo.Application.Common.Exceptions;
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Entities;
    using CleanCli.Todo.Domain.Enums;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateTodoItemDetailCommand : IRequest
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context) => this.context = context;

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context.TodoItems.FindAsync(
                new object[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
