using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Process
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly DbConnection db;

        public ProcessRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<PROCESS> AddProcess(PROCESS process)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addProcess = await conn.QueryAsync<PROCESS>("PRO.SP_ADD_PROCESS",
                    new {
                        @IDRULE = process.IDRULE,
                        @CODEPROCESS = process.CODEPROCESS,
                        @PROCESSNAME = process.PROCESSNAME,
                        @CHARGE_PERSON = process.CHARGE_PERSON,
                        @ROLE_INVOLVES = process.ROLE_INVOLVES
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingProcess(addProcess);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<PROCESS>> GetAllProcess()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllProcess = await conn.QueryAsync<PROCESS, ISORULE, PROCESS>("PRO.SP_GET_ALL_PROCESS",
                    map: (process, isorule) => { process.ISORULE = isorule; return process; },
                    splitOn:"IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();   
                conn.Dispose();
                return getAllProcess.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<PROCESS> GetProcessById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getProcessById = await conn.QueryAsync<PROCESS, ISORULE, PROCESS>("PRO.SP_GET_PROCESS_BY_ID",
                    map: (process, isorule) => { process.ISORULE = isorule; return process; },
                    new { @IDPROCESS = id },
                    splitOn: "IDRULE",
                commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result= MappingProcess(getProcessById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<PROCESS> RemoveProcess(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeProcess = await conn.QueryAsync<PROCESS, ISORULE, PROCESS>("PRO.SP_REMOVE_PROCESS",
                    map: (process, isorule) => { process.ISORULE = isorule; return process; },
                    new {@IDPROCESS =  id},
                    splitOn: "IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                var result= MappingProcess(removeProcess);  
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<PROCESS> UpdateProcess(PROCESS process, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                var updateProcess = await conn.QueryAsync<PROCESS, ISORULE, PROCESS>("PRO.SP_UPDATE_PROCESS",
                    map: (process, isorule) => { process.ISORULE = isorule; return process; },
                    new
                {
                        @IDPROCESS = id,
                    @IDRULE = process.IDRULE,
                    @CODEPROCESS = process.CODEPROCESS,
                    @PROCESSNAME =  process.PROCESSNAME,
                    @CHARGE_PERSON =process.CHARGE_PERSON,
                    @ROLE_INVOLVES = process.ROLE_INVOLVES
                },
                    splitOn: "IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingProcess(updateProcess);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private PROCESS MappingProcess(IEnumerable<PROCESS> processList)
        {
            PROCESS process = new PROCESS();
            foreach (var item in processList)
            {
                process.IDPROCESS = item.IDPROCESS;
                process.IDRULE = item.IDRULE;
                process.CODEPROCESS = item.CODEPROCESS;
                process.PROCESSNAME = item.PROCESSNAME;
                process.CHARGE_PERSON = item.CHARGE_PERSON; 
                process.ROLE_INVOLVES = item.ROLE_INVOLVES;
                process.CREATEDATE = item.CREATEDATE;
                process.UPDATEDATE = item.UPDATEDATE;
                process.ISORULE = item.ISORULE;
            }
            return process;
        }
    }
}
