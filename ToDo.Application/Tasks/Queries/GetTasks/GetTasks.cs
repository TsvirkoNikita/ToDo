using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<GetTasksResponse>
    {
        public bool? Completed { get; init; }
    }

    public record GetTasksResponse(IEnumerable<TaskDTO> Tasks, int CountOfActive);

    public class GetTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
        : IRequestHandler<GetTasksQuery, GetTasksResponse>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<GetTasksResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tasks.AsNoTracking();

            if (request.Completed.HasValue)
            {
                query = query.Where(x => x.Completed == request.Completed);
            }

            var tasks = await query.ProjectTo<TaskDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            var countOfActive = await _context.Tasks.CountAsync(t => !t.Completed, cancellationToken);
            
            var result = new GetTasksResponse(tasks, countOfActive);

            return result;
        }
    }
}
