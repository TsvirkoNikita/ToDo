using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Tasks.Commands.CreateTask;
using ToDo.Application.Tasks.Commands.DeleteAllCompletedTasks;
using ToDo.Application.Tasks.Commands.DeleteTask;
using ToDo.Application.Tasks.Commands.UpdateTask;
using ToDo.Application.Tasks.Queries.GetTask;
using ToDo.Application.Tasks.Queries.GetTasks;

namespace ToDo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<int> CreateTask([FromBody] CreateTaskCommand createTaskCommand)
        {
            return await _sender.Send(createTaskCommand);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GetTasksResponse> GetTasks([FromQuery] GetTasksQuery getTasksQuery)
        {
            return await _sender.Send(getTasksQuery);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<TaskDTO> GetTask([FromRoute] int id)
        {
            return await _sender.Send(new GetTaskQuery(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskCommand updateTaskCommand)
        {
            if (id != updateTaskCommand.Id) return Results.BadRequest();
            await _sender.Send(updateTaskCommand);
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteTask([FromRoute] int id)
        {
            await _sender.Send(new DeleteTaskCommand(id));
            return Results.NoContent();
        }

        [HttpDelete("DeleteAllCompletedTasks")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IResult> DeleteAllCompletedTasks()
        {
            await _sender.Send(new DeleteAllCompletedTasksCommand());
            return Results.NoContent();
        }
    }
}
