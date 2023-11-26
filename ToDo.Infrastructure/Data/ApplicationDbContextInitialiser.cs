using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Data
{
    public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Tasks.Any())
            {
                await _context.Tasks.AddRangeAsync([
                    new TaskEntity
                    {
                        Title = "Test Task",
                        Description = "Description",
                        Completed = true,
                    },
                    new TaskEntity
                    {
                        Title = "Important Task",
                        Description = "Very Important Description",
                        Completed = false,
                    },
                    new TaskEntity
                    {
                        Title = "Lorem Ipsum",
                        Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                        Completed = false,
                    }
                ]);

                await _context.SaveChangesAsync();
            }
        }
    }
}
