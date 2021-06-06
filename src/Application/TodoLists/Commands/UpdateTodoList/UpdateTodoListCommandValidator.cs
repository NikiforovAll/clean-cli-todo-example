namespace CleanCli.Todo.Application.TodoLists.Commands.UpdateTodoList
{
    using CleanCli.Todo.Application.Common.Interfaces;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateTodoListCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            this.RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(this.BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken) =>
            await this.context.TodoLists
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Title != title, cancellationToken: cancellationToken);
    }
}
