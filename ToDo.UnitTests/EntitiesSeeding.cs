using ToDo.Domain.Entities;

namespace ToDo.UnitTests
{
    public static class EntitiesSeeding
    {
        public static List<TaskEntity> GetTasks()
        {
            return
            [
                new TaskEntity
                {
                    Id = 1,
                    Title = "test",
                    Description = "description",
                    Completed = true
                },
                new TaskEntity
                {
                    Id = 2,
                    Title = "test",
                    Description = "description",
                    Completed = false
                },
                new TaskEntity
                {
                    Id = 3,
                    Title = "test",
                    Description = "description",
                    Completed = true
                }
            ];
        }
    }
}
