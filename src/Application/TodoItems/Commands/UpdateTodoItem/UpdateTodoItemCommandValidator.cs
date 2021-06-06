namespace CleanCli.Todo.Application.TodoItems.Commands.UpdateTodoItem
{
    using FluentValidation;

    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator() =>
            this.RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
    }
}
