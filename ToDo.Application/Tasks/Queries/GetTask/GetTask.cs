using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Tasks.Queries.GetTasks;

namespace ToDo.Application.Tasks.Queries.GetTask
{
    public record GetTaskQuery(int Id) : IRequest<TaskDTO>;

    public class GetTaskQueryHandler(IApplicationDbContext context, IMapper mapper)
        : IRequestHandler<GetTaskQuery, TaskDTO>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskDTO> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.AsNoTracking()
                .Where(t => t.Id == request.Id)
                .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(request.Id);

            return task;
        }
    }
}
