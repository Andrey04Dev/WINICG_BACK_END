using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.No_Accordance
{
    public class NoAccordanceRepository : INoAccordanceRepository
    {
        private readonly DbConnection db;

        public NoAccordanceRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<NO_ACCORDANCE> AddNoAccordance(NO_ACCORDANCE accordance)
        {
            try
            {
                using var conn = db.GetConnection(); 
                conn.Open();
                var addAccordance = await conn.QueryAsync<NO_ACCORDANCE>("PRO.SP_ADD_NO_ACCORDANCE",
                    new{
                        @IDPROCESS = accordance.IDPROCESS,
                        @IDAUDIT = accordance.IDAUDIT,
                        @IDTASK = accordance.IDTASK,
                        @NAME_NO_ACCORDANCE = accordance.NAME_NO_ACCORDANCE,
                        @KIND = accordance.KIND,
                        @RANKING = accordance.RANKING,
                        @DESCRIPTON = accordance.DESCRIPTION,
                        @AUDIT_DETECT = accordance.AUDIT_DETECT
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingNoAccordance(addAccordance);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<NO_ACCORDANCE>> GetAllNoAccordance()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllNoAccordance = await conn.QueryAsync<NO_ACCORDANCE, PROCESS, AUDITS,TASKS, NO_ACCORDANCE>("PRO.SP_GET_ALL_NO_ACCORDANCE",
                    map: (accordance, process, audits, task) => { accordance.PROCESS = process; accordance.AUDITS = audits; accordance.TASKS = task; return accordance; },
                    splitOn:"IDPROCESS,IDAUDIT,IDTASK",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<NO_ACCORDANCE>)getAllNoAccordance;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<NO_ACCORDANCE> GetNoAccordanceById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAccordanceById = await conn.QueryAsync<NO_ACCORDANCE, PROCESS, AUDITS, TASKS, NO_ACCORDANCE>("PRO.SP_GET_NO_ACCORDANCE_BY_ID",
                    map: (accordance, process, audits, task) => { accordance.PROCESS = process; accordance.AUDITS = audits; accordance.TASKS = task; return accordance; },
                    new { @IDACCORDANCE= id },
                    splitOn: "IDPROCESS,IDAUDIT,IDTASK",
                    commandType: System.Data.CommandType.StoredProcedure);
                var result =  MappingNoAccordance(getAccordanceById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<NO_ACCORDANCE> RemoveNoAccordance(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeAccordance = await conn.QueryAsync<NO_ACCORDANCE, PROCESS, AUDITS, TASKS, NO_ACCORDANCE>("PRO.SP_REMOVE_NO_ACCORDANCE",
                    map: (accordance, process, audits, task) => { accordance.PROCESS = process; accordance.AUDITS = audits; accordance.TASKS = task; return accordance; },
                    new {@IDACCORDANCE = id },
                    splitOn: "IDPROCESS,IDAUDIT,IDTASK"
                , commandType: System.Data.CommandType.StoredProcedure);
                var result  =  MappingNoAccordance(removeAccordance);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<NO_ACCORDANCE> UpdateNoAccordance(NO_ACCORDANCE accordance, string id)
        {
            using var conn = db.GetConnection();  
            conn.Open();
            var updateAccordance = await conn.QueryAsync<NO_ACCORDANCE, PROCESS, AUDITS, TASKS, NO_ACCORDANCE>("PRO.SP_UPDATE_NO_ACCORDANCE",
                map: (accordance, process, audits, task) => { accordance.PROCESS = process; accordance.AUDITS = audits; accordance.TASKS = task; return accordance; },
                new
                {
                    @IDACCORDANCE = id,
                    @IDPROCESS = accordance.IDPROCESS,
                    @IDAUDIT = accordance.IDAUDIT,
                    @IDTASK = accordance.IDTASK,
                    @NAME_NO_ACCORDANCE = accordance.NAME_NO_ACCORDANCE,
                    @KIND = accordance.KIND,
                    @RANKING = accordance.RANKING,
                    @DESCRIPTON =  accordance.DESCRIPTION,
                    @STATE =  accordance.STATE,
                    @AUDIT_DETECT = accordance.AUDIT_DETECT
                },
                splitOn: "IDPROCESS,IDAUDIT,IDTASK",
                    commandType: System.Data.CommandType.StoredProcedure);
            var result = MappingNoAccordance(updateAccordance);
            return result;
        }

        private NO_ACCORDANCE MappingNoAccordance(IEnumerable<NO_ACCORDANCE> No_AccordanceList)
        {
            NO_ACCORDANCE NO_ACCORDANCE = new NO_ACCORDANCE();
            foreach (var item in No_AccordanceList)
            {
                NO_ACCORDANCE.IDACCORDANCE = item.IDACCORDANCE;
                NO_ACCORDANCE.IDPROCESS = item.IDPROCESS;
                NO_ACCORDANCE.IDTASK = item.IDTASK;
                NO_ACCORDANCE.NAME_NO_ACCORDANCE = item.NAME_NO_ACCORDANCE;
                NO_ACCORDANCE.KIND = item.KIND;
                NO_ACCORDANCE.AUDIT_DETECT = item.AUDIT_DETECT;
                NO_ACCORDANCE.RANKING = item.RANKING;
                NO_ACCORDANCE.DESCRIPTION = item.DESCRIPTION;
                NO_ACCORDANCE.STATE = item.STATE;
                NO_ACCORDANCE.CREATEDATE = item.CREATEDATE;
                NO_ACCORDANCE.UPDATEDATE = item.UPDATEDATE;
                NO_ACCORDANCE.PROCESS = item.PROCESS;
                NO_ACCORDANCE.AUDITS = item.AUDITS;
            }
            return NO_ACCORDANCE;
        }
    }
}
