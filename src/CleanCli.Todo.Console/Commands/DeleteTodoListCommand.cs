namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Linq;
    using System.Threading.Tasks;
    using Spectre.Console;
    using Spectre.Console.Rendering;

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
            public Task<int> InvokeAsync(InvocationContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}

