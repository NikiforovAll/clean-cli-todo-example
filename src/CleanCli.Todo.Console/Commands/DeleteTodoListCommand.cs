namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using MediatR;

    public class DeleteTodoListCommand : Command
    {
        public IConsole Console { get; set; }

        public DeleteTodoListCommand()
            : base(name: "delete", "Deletes todo list")
        {
            this.AddArgument(new Argument<int>("id", "Id of the todo list"));
        }

        public new class Handler : ICommandHandler
        {
            private readonly IMediator meditor;

            public int Id { get; set; }

            public Handler(IMediator meditor) =>
                this.meditor = meditor ?? throw new ArgumentNullException(nameof(meditor));

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                await this.meditor.Send(new Application.TodoLists.Commands.DeleteTodoList.DeleteTodoListCommand
                {
                    Id = this.Id,
                });

                return 0;
            }
        }
    }
}

