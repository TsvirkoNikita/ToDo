using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Interfaces;

namespace ToDo.Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<IEnumerable<TaskDTO>>
    {
        public bool? Completed { get; init; }
    }

    public class GetTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
        : IRequestHandler<GetTasksQuery, IEnumerable<TaskDTO>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TaskDTO>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tasks.AsNoTracking();

            if (request.Completed.HasValue)
            {
                query = query.Where(x => x.Completed == request.Completed);
            }

            return await query.ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
