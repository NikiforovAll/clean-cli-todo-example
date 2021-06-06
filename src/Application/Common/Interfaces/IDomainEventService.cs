namespace CleanCli.Todo.Application.Common.Interfaces
{
    using CleanCli.Todo.Domain.Common;
    using System.Threading.Tasks;

    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
