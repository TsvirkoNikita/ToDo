using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaskEntity> Tasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
