namespace CleanCli.Todo.Application.TodoLists.Commands.CreateTodoList
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IApplicationDbContext context;

        public CreateTodoListCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            this.RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(this.BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken) =>
            await this.context.TodoLists.AllAsync(l => l.Title != title, cancellationToken: cancellationToken);
    }
}
