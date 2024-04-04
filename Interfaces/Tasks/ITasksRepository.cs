using webapi.Models;

namespace webapi.Interfaces.Tasks
{
    public interface ITasksRepository
    {
        Task<TASKS> AddTasks(TASKS tasks);
        Task<TASKS> RemoveTasks(string id);
        Task<TASKS> UpdateTasks(TASKS tasks, string id);
        Task<List<TASKS>> GetAllTasks();
        Task<TASKS> GetTasksById(string id);
        Task<int> GetCountTask();
    }
}
