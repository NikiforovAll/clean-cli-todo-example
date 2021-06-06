namespace CleanCli.Todo.Console.Commands
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using CleanCli.Todo.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    public class MigrateCommand : Command
    {
        public MigrateCommand()
            : base(name: "migrate", "Migrates database")
        {
        }

        public new class Handler : ICommandHandler
        {
            private readonly ApplicationDbContext db;

            public Handler(ApplicationDbContext db) => this.db = db;

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                this.db.Database.EnsureCreated();
                this.db.Database.Migrate();
                await ApplicationDbContextSeed.SeedSampleDataAsync(this.db);
                return 0;
            }
        }
    }
}
