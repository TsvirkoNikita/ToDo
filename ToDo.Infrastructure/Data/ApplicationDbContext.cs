using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
