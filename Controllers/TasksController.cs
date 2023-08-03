using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces.Tasks;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRepository tasks;

        public TasksController(ITasksRepository tasks)
        {
            this.tasks = tasks;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            try
            {
                var getTask = await tasks.GetAllTasks();
                if (getTask == null) { return BadRequest("There is not a data!"); }
                return Ok(getTask);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"The id is empty! ");
                }
                var getTaskById = await tasks.GetTasksById(id);
                if (getTaskById == null)
                {
                    return BadRequest("There is not a data");
                }
                return Ok(getTaskById);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromForm] TASKS TASKSDTO)
        {
            try
            {
                if (TASKSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var addTASKS = await tasks.AddTasks(TASKSDTO);

                if (addTASKS == null) return BadRequest("There is a problem with the data!");
                return Ok(addTASKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromForm] TASKS TASKSDTO, string id)
        {
            try
            {
                if (TASKSDTO == null)
                {
                    return BadRequest("THe data is empty");
                }
                var updateTASKS = await tasks.UpdateTasks(TASKSDTO, id);

                if (updateTASKS == null) return BadRequest("There is a problem with the data!");
                return Ok(updateTASKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTask(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is empty");
                }
                var deleteTASKS = await tasks.RemoveTasks(id);

                if (deleteTASKS == null) return BadRequest("There is a problem with the data!");
                return Ok(deleteTASKS);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
