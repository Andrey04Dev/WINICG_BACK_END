using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Tasks
{
    public class TasksRepository : ITasksRepository
    {
        private readonly DbConnection db;

        public TasksRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<TASKS> AddTasks(TASKS tasks)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addTask = await conn.QueryAsync<TASKS>("ISO.SP_ADD_TASK", 
                    new {
                        @IDUSER=tasks.IDUSER,
                        @IDRULE =tasks.IDRULE,
                        @IDFLAG=tasks.IDFLAG,
                        @PROJECT=tasks.PROJECT,
                        @EVENT_TASK=tasks.EVENT_TASK}, 
                    commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingTasks(addTask);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<TASKS>> GetAllTasks()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllTask = await conn.QueryAsync<TASKS>("ISO.SP_GET_ALL_TASK", commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return getAllTask.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<TASKS> GetTasksById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getTaskById = await conn.QueryAsync<TASKS>("ISO.SP_GET_TASK_BY_ID", new { @IDTASK = id }, 
                    commandType:  System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingTasks(getTaskById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<TASKS> RemoveTasks(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeTask = await conn.QueryAsync<TASKS>("ISO_REMOVE_TASK", new {@IDTASKS = id}, commandType: System.Data.CommandType.StoredProcedure );
                conn.Close();
                conn.Dispose();
                var result = MappingTasks(removeTask);
                return result;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<TASKS> UpdateTasks(TASKS tasks, string id)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var updateTask = await conn.QueryAsync<TASKS>("ISO.SP_UPDATE_TASK "
                ,new {
                        @IDUSER = tasks.IDUSER,
                        @IDRULE = tasks.IDRULE,
                        @IDFLAG = tasks.IDFLAG,
                        @PROJECT = tasks.PROJECT,
                        @EVENT_TASK = tasks.EVENT_TASK}, 
                    commandType: System.Data.CommandType.StoredProcedure);
            var result = MappingTasks(updateTask);
            return result;
        }
        private TASKS MappingTasks(IEnumerable<TASKS> tasksList)
        {
            TASKS task = new TASKS();
            foreach (var item in tasksList)
            {
                task.IDTASK = item.IDTASK;
                task.IDRULE = item.IDRULE;
                task.IDFLAG = item.IDFLAG;
                task.IDUSER = item.IDUSER;
                task.PROJECT = item.PROJECT;
                task.EVENT_TASK = item.EVENT_TASK;
                task.CREATEDATE = item.CREATEDATE;
                task.UPDATEDATE = item.UPDATEDATE;
                task.USERS= item.USERS;
                task.FLAGS= item.FLAGS;
                task.ISORULE = item.ISORULE;
            }
            return task;
        }
    }
}
