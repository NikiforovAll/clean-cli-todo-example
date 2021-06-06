namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using MediatR;

    public class CreateTodoListCommand : Command
    {
        public IConsole Console { get; set; }

        public CreateTodoListCommand()
            : base(name: "create", "Creates todo list")
        {
            this.AddOption(new Option<string>(
                new string[] { "--title", "-t" }, "Title of the todo list"));
            this.AddOption(new Option<bool>(
                "--dry-run", "Displays a summary of what would happen if the given command line were run."));
        }

        public new class Handler : ICommandHandler
        {
            private readonly IMediator meditor;

            public string Title { get; set; }

            public Handler(IMediator meditor) =>
                this.meditor = meditor ?? throw new ArgumentNullException(nameof(meditor));

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                await this.meditor.Send(
                    new Application.TodoLists.Commands.CreateTodoList.CreateTodoListCommand
                    {
                        Title = this.Title,
                    });

                return 0;
            }
        }
    }
}
