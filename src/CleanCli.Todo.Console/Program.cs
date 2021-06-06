using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using CleanCli.Todo.Application;
using CleanCli.Todo.Console;
using CleanCli.Todo.Console.Commands;
using CleanCli.Todo.Infrastructure;
using Microsoft.Extensions.Hosting;
using Serilog;

var runner = BuildCommandLine()
    .UseHost(_ => CreateHostBuilder(args), (builder) => builder
        .UseEnvironment("CLI")
        .UseSerilog()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSerilog();
            var configuration = hostContext.Configuration;
            services.AddCli();
            services.AddApplication();
            services.AddInfrastructure(configuration);
        })
        .UseCommandHandler<CreateTodoListCommand, CreateTodoListCommand.Handler>()
        .UseCommandHandler<DeleteTodoListCommand, DeleteTodoListCommand.Handler>()
        .UseCommandHandler<ListTodosCommand, ListTodosCommand.Handler>()
        .UseCommandHandler<GetTodoListCommand, GetTodoListCommand.Handler>()
        .UseCommandHandler<SeedTodoItemsCommand, SeedTodoItemsCommand.Handler>()
        .UseCommandHandler<MigrateCommand, MigrateCommand.Handler>()).UseDefaults().Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new RootCommand();
    root.AddCommand(BuildTodoListCommands());
    root.AddCommand(BuildTodoItemsCommands());
    root.AddCommand(new MigrateCommand());
    root.AddGlobalOption(new Option<bool>("--silent", "Disables diagnostics output"));
    root.Handler = CommandHandler.Create(() => root.Invoke("-h"));


    return new CommandLineBuilder(root);

    static Command BuildTodoListCommands()
    {
        var todolist = new Command("todolist", "Todo lists management")
        {
            new CreateTodoListCommand(),
            new DeleteTodoListCommand(),
            new GetTodoListCommand(),
            new ListTodosCommand(),
        };
        return todolist;
    }

    static Command BuildTodoItemsCommands()
    {
        var todoitem = new Command("todoitem", "Todo items management")
        {
            new SeedTodoItemsCommand()
        };
        return todoitem;
    }
}

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);

