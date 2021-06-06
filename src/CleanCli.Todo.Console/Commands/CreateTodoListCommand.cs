namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Spectre.Console;
    using Spectre.Console.Rendering;

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
            private readonly ILogger<Handler> logger;

            public Handler(ILogger<Handler> logger)
            {
                this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }
            public Task<int> InvokeAsync(InvocationContext context)
            {
                this.logger.LogInformation("About to insert TodoList into storage");
                throw new NotImplementedException();
            }
        }
    }
}
