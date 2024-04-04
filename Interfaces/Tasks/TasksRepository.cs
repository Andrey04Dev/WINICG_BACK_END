using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                        @IDFLAGS=tasks.IDFLAG,
                        @PROJECT=tasks.PROJECT,
                        @EVENT_TASK=tasks.EVENT_TASK}, 
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
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
                //TASKS tasksEmpty = null;
                using var conn = db.GetConnection();
                conn.Open();
                var getAllTask = await conn.QueryAsync<TASKS, USERS, ISORULE, FLAGS,TASKS>("ISO.SP_GET_ALL_TASK",
                    map: (task,users, isorules,flags) =>{
                        //tasksEmpty = task;
                        task.USERS = users;
                        task.ISORULE = isorules;
                        task.FLAGS=flags;  
                        return task; },
                   // map: (task, users,isorule, flags) => { task.USERS = users; task.ISORULE = isorule; task.FLAGS = flags;  return task; },
                    splitOn: "IDUSER, IDRULE, IDFLAG",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return getAllTask.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> GetCountTask()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM ISO.TASKS";
            var GetAudit = await conn.QueryAsync<int>(sql);
            conn.Close();
            conn.Dispose();
            var result = 0;
            foreach (var audit in GetAudit)
            {
                result = audit;
            }
            return result;
        }

        public async Task<TASKS> GetTasksById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getTaskById = await conn.QueryAsync<TASKS, USERS, ISORULE, FLAGS, TASKS>("ISO.SP_GET_TASK_BY_ID",
                    map: (task, users, isorule, flag) => { task.USERS = users; task.ISORULE = isorule; task.FLAGS = flag; return task; },
                    new { @IDTASK = id },
                    splitOn: "IDUSER,IDRULE,IDFLAG",
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
                var removeTask = await conn.QueryAsync<TASKS, USERS, ISORULE, FLAGS, TASKS>("ISO_REMOVE_TASK",
                    map: (task, users, isorule, flag) => { task.USERS = users; task.ISORULE = isorule; task.FLAGS = flag; return task; },
                    new {@IDTASKS = id},
                    splitOn: "IDUSER,IDRULE,IDFLAG",
                    commandType: System.Data.CommandType.StoredProcedure );
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
            var updateTask = await conn.QueryAsync<TASKS, USERS, ISORULE, FLAGS, TASKS>("ISO.SP_UPDATE_TASK ",
                map: (task, users, isorule, flag) => { task.USERS = users; task.ISORULE = isorule; task.FLAGS = flag; return task; }
                , new {
                        @IDTASKS= id,
                        @IDUSER = tasks.IDUSER,
                        @IDRULE = tasks.IDRULE,
                        @IDFLAGS = tasks.IDFLAG,
                        @PROJECT = tasks.PROJECT,
                        @EVENT_TASK = tasks.EVENT_TASK},
                splitOn: "IDUSER,IDRULE,IDFLAG",
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
                task.PERSONCHANGE = item.PERSONCHANGE;
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
