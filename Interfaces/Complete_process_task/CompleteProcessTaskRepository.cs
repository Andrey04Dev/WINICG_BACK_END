using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Complete_process_task
{
    public class CompleteProcessTaskRepository : ICompleteProcessTaskRepository
    {
        private readonly DbConnection db;

        public CompleteProcessTaskRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<COMPLETE_PROCESS_TASK> AddCompleteTask(COMPLETE_PROCESS_TASK completeProcessTask)
        {
            try
            {
                //var addCompleteTask = await db.Complete_Process_Task.FromSqlRaw()
                return completeProcessTask;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Task<List<COMPLETE_PROCESS_TASK>> GetAllCompleteTask()
        {
            throw new NotImplementedException();
        }

        public Task<COMPLETE_PROCESS_TASK> GetCompleteTaskById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<COMPLETE_PROCESS_TASK> RemoveCompleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public Task<COMPLETE_PROCESS_TASK> UpdateCompleteTask(COMPLETE_PROCESS_TASK completeProcessTask, string id)
        {
            throw new NotImplementedException();
        }
    }
}
