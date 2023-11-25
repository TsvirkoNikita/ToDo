using AutoMapper;
using ToDo.Domain.Entities;

namespace ToDo.Application.Tasks.Queries.GetTasks
{
    public class TaskDTO
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public bool Completed { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TaskEntity, TaskDTO>();
            }
        }
    }
}
