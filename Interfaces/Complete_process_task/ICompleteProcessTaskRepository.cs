using webapi.Models;

namespace webapi.Interfaces.Complete_process_task
{
    public interface ICompleteProcessTaskRepository
    {
        Task<COMPLETE_PROCESS_TASK> AddCompleteTask(COMPLETE_PROCESS_TASK completeProcessTask);
        Task<COMPLETE_PROCESS_TASK> RemoveCompleteTask(string id);
        Task<COMPLETE_PROCESS_TASK> UpdateCompleteTask(COMPLETE_PROCESS_TASK completeProcessTask, string id);
        Task<List<COMPLETE_PROCESS_TASK>> GetAllCompleteTask();
        Task<COMPLETE_PROCESS_TASK> GetCompleteTaskById(string id);
    }
}
