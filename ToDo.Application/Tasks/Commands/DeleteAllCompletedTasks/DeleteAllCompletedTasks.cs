using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Tasks.Commands.DeleteAllCompletedTasks
{
    public record DeleteAllCompletedTasksCommand : IRequest;

    public class DeleteAllCompletedTasksCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteAllCompletedTasksCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(DeleteAllCompletedTasksCommand request, CancellationToken cancellationToken)
        {
            await _context.Tasks.Where(t => t.Completed).ExecuteDeleteAsync(cancellationToken);
        }
    }
}
