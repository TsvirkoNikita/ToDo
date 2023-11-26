using ToDo.Application.Tasks.Commands.CreateTask;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Data;

namespace ToDo.UnitTests.Tasks.Commands
{
    public class CreateTaskCommandHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateTaskCommandHandler _handler;

        public CreateTaskCommandHandlerTests()
        {
            _context = DbContextMock
                .GetMock<TaskEntity, ApplicationDbContext>(EntitiesSeeding.GetTasks(), x => x.Tasks);

            _handler = new CreateTaskCommandHandler(_context);
        }

        [Fact]
        public async Task Valid_Task_Added()
        {
            //Arrange
            var command = new CreateTaskCommand() { Title = "Test" };

            //Act
            await _handler.Handle(command, CancellationToken.None);
            var tasksCount = _context.Tasks.Count();

            //Assert
            Assert.Equal(4, tasksCount);
        }
    }
}
