namespace CleanCli.Todo.Console.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using CleanCli.Todo.Application.TodoLists.Queries.GetTodos;
    using MediatR;
    using Spectre.Console;

    public class ListTodosCommand : Command
    {
        public IConsole Console { get; set; }

        public ListTodosCommand()
            : base(name: "list", "Lists all todo lists in the system.")
        {
        }

        public new class Handler : ICommandHandler
        {
            private readonly IMediator meditor;

            public Handler(IMediator meditor) =>
                this.meditor = meditor ?? throw new ArgumentNullException(nameof(meditor));

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                var lists = (await this.meditor.Send(new GetTodosQuery { }))?.Lists;
                var table = new Table() { Title = new TableTitle($"Todo Lists ({lists?.Count})") };
                _ = table.AddColumn("Id");
                _ = table.AddColumn("Title");

                foreach (var list in lists)
                {
                    table.AddRow(list.Id.ToString(CultureInfo.InvariantCulture), list.Title);
                }
                var barchart = new BarChart()
                    .Width(60)
                    .Label($"[green bold underline]Summary[/]")
                    .CenterLabel()
                    .AddItem("Lists", lists.Count, Color.Orange1)
                    .AddItem("Items", lists.SelectMany(l => l.Items).Count(), Color.Orange4);

                var grid = new Grid().Alignment(Justify.Center);
                grid.AddColumn(new GridColumn());
                grid.AddRow(table);
                grid.AddRow(barchart);

                AnsiConsole.Render(new Panel(grid));

                return 0;
            }
        }
    }
}
