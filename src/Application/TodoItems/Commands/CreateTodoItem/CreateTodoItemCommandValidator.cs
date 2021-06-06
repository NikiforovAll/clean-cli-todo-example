namespace CleanCli.Todo.Application.TodoItems.Commands.CreateTodoItem
{
    using FluentValidation;

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator() =>
            this.RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
    }
}
