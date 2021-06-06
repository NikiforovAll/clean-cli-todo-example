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

    public class ListTodosCommand : Command
    {
        public IConsole Console { get; set; }

        public ListTodosCommand()
            : base(name: "list", "Lists all todo lists in the system.")
        {
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
