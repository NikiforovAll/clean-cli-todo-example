namespace CleanCli.Todo.Infrastructure.Services
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using System;

    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
