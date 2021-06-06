namespace CleanCli.Todo.Application.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.") => this.Errors = new Dictionary<string, string[]>();

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this() => this.Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        public IDictionary<string, string[]> Errors { get; }
    }
}
