using MediatR;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest<int>
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
    }

    public class CreateTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new TaskEntity
            {
                Title = request.Title,
            };

            _context.Tasks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
