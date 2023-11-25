using FluentValidation;

namespace ToDo.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty();

            RuleFor(t => t.Title)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(t => t.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
