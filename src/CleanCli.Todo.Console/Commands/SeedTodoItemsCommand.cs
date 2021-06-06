namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;

    public class SeedTodoItemsCommand : Command
    {
        public IConsole Console { get; set; }

        public SeedTodoItemsCommand()
            : base(name: "seed", "Seeds a random todo items.")
        {
            this.AddOption(new Option<string>(
                new string[] { "--todolist", "-l" }, "Title of the todo list"));
            this.AddOption(new Option<int>(
                new string[] { "--number", "-n" },
                () => 3,
                "The number of todo items to be generated."));
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
