# CleanCli.Todo.Example

![GitHub Actions Status](https://github.com/nikiforovall/clean-cli-todo-example/workflows/Build/badge.svg?branch=main)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

[![GitHub Actions Build History](https://buildstats.info/github/chart/nikiforovall/clean-cli-todo-example?branch=main&includeBuildsFromPullRequest=false)](https://github.com/nikiforovall/clean-cli-todo-example/actions)

An example of how to use Clean Architecture together with CLI applications.

## Goals

* To use a CLI as UI for a Clean Architecture based solutions. As result, you can use CLI as standalone implementation or just auxiliary project to perform a project tasks.
* To design CLI application that clearly communicates implemented functionality (commands) and structural composition in general.
* To provide first class user experience through CLI implementation best practices. Luckily, `System.CommandLine` help with things such as "help text" generation and autocompletion.

```csharp
var runner = BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(args), (builder) =>
    {
        builder.UseEnvironment("CLI")
        .UseSerilog()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSerilog();
        })
        .UseCommandHandler<CreateTodoListCommand, CreateTodoListCommand.Handler>()
        .UseCommandHandler<DeleteTodoListCommand, DeleteTodoListCommand.Handler>()
        .UseCommandHandler<ListTodosCommand, ListTodosCommand.Handler>()
        .UseCommandHandler<GetTodoListsCommand, GetTodoListsCommand.Handler>()
        .UseCommandHandler<SeedTodoItemsCommand, SeedTodoItemsCommand.Handler>();
    }).UseDefaults().Build();

return await runner.InvokeAsync(args);
```

## Technologies

| Dependencies                                 | Description                                            |
|----------------------------------------------|--------------------------------------------------------|
| <https://github.com/dotnet/command-line-api> | Command execution, provides command/handler app model. |
| <https://spectreconsole.net/>                | Beautiful console apps                                 |

The application is based on popular <https://github.com/jasontaylordev/CleanArchitecture> template.

## Commands Overview

Top level commands:

```bash
$ dotnet run -- -h
CleanCli.Todo.Console

Usage:
  CleanCli.Todo.Console [options] [command]

Options:
  --silent        Disables diagnostics output
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  todolist  Todo lists management
  todoitem  Todo items management
```

Todo List commands:

```bash
$ dotnet run -- todolist -h
todolist
  Todo lists management

Usage:
  CleanCli.Todo.Console [options] todolist [command]

Options:
  --silent        Disables diagnostics output
  -?, -h, --help  Show help and usage information

Commands:
  create  Creates todo list
  delete  Deletes todo list
  get     Gets a todo list
  list    Lists all todo lists in the system.

# command details example
$ dotnet run -- todolist create -h
create
  Creates todo list

Usage:
  CleanCli.Todo.Console [options] todolist create

Options:
  -t, --title <title>  Title of the todo list
  --dry-run            Displays a summary of what would happen if the given command line were run.
  --silent             Disables diagnostics output
  -?, -h, --help       Show help and usage information
```

Todo Items commands:

```bash
$ dotnet run -- todoitem -h
todoitem
  Todo items management

Usage:
  CleanCli.Todo.Console [options] todoitem [command]

Options:
  --silent        Disables diagnostics output
  -?, -h, --help  Show help and usage information

Commands:
  seed  Seeds a random todo items.

# command details example
$ dotnet run -- todoitem seed -h
seed
  Seeds a random todo items.

Usage:
  CleanCli.Todo.Console [options] todoitem seed

Options:
  -l, --todolist <todolist>  Title of the todo list
  -n, --number <number>      The number of todo items to be generated. [default: 3]
  --silent                   Disables diagnostics output
  -?, -h, --help             Show help and usage information
```
