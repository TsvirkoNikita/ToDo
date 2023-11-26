using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public bool Completed { get; init; }
    }

    public class UpdateTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(request.Id);

            task.Title = request.Title;
            task.Description = request.Description;
            task.Completed = request.Completed;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
