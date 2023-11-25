using FluentValidation;

namespace ToDo.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(t => t.Title)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(t => t.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
