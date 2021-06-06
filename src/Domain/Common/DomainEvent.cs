namespace CleanCli.Todo.Domain.Common
{
    using System;
    using System.Collections.Generic;

    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent() => this.DateOccurred = DateTimeOffset.UtcNow;
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
