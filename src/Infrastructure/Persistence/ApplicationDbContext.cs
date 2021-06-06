namespace CleanCli.Todo.Infrastructure.Persistence
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Common;
    using CleanCli.Todo.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime dateTime;
        private readonly IDomainEventService domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            this.domainEventService = domainEventService;
            this.dateTime = dateTime;
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in this.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = this.dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = this.dateTime.Now;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await this.DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = this.ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null)
                {
                    break;
                }

                domainEventEntity.IsPublished = true;
                await this.domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
