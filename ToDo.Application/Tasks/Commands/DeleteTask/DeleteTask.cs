using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int Id) : IRequest;

    public class DeleteTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(request.Id);

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
