using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.DTO.Audits;
using webapi.Models;

namespace webapi.Interfaces.Audits
{
    public class AuditRepository : IAuditRepository
    {
        private readonly DbConnection connection;
        private readonly IMapper mapper;

        public AuditRepository(DbConnection connection, IMapper mapper)
        {
            this.connection = connection;
            this.mapper = mapper;
        }
        public  async Task<AUDITS> AddAudits(AUDITS audits)
        {
            using var conn = connection.GetConnection();
            conn.Open();
            var AuditAdded = await conn.QueryAsync<AUDITS>($"ISO.SP_ADD_AUDIT",
                new {@AUDIT_NAME = audits.AUDIT_NAME, @AUDIT_DATE =audits.AUDIT_DATE, @AUDIT_TIME = audits.AUDIT_TIME,
                @AUDIT_SUBJECT = audits.AUDIT_SUBJECT, @NUMBER_DAYS = audits.NUMBER_DAYS, @KIND_AUDIT = audits.KIND_AUDIT,
                @SCOPE_AUDIT = audits.SCOPE_AUDIT, @AUDIT_PROCESS= audits.AUDIT_PROCESS, @AUDIT_RULE=audits.AUDIT_RULE}, 
                commandType:  CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose();
            var result = MappingAudit(AuditAdded);
            return result;
        }

        public async Task<List<AUDITS>> GetAllAudits()
        {
            using var conn = connection.GetConnection();
            conn.Open();
            var GetAudit = await  conn.QueryAsync<AUDITS>("ISO.SP_GET_ALL_AUDITS", 
                commandType: CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose( );
            return (List<AUDITS>)GetAudit;
        }

        public async Task<AUDITS> GetAuditsById(string id)
        {
            using var conn = connection.GetConnection();
            conn.Open();
            var GetAudit = await conn.QueryAsync<AUDITS>("ISO.SP_GET_AUDITS_BY_ID", new { @IDAUDIT = id}, commandType: CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose( );    
            var result = MappingAudit(GetAudit);
            return result;
        }

        public async Task<AUDITS> RemoveAudits(string id)
        {

            try
            {
                using var conn = connection.GetConnection();
                conn.Open();
                var DeleteAudit = await conn.QueryAsync<AUDITS>("ISO.SP_DELETE_AUDIT", new { @IDAUDIT = id}, commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingAudit(DeleteAudit);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<AUDITS> UpdateAudits(AUDITS audits, string id)
        {
            try
            {
                using var conn = connection.GetConnection();
                conn.Open();
                var DeleteAudit = await conn.QueryAsync<AUDITS>("ISO.SP_UPDATE_AUDIT", new
                {
                    @IDAUDITS= id,
                    @AUDIT_NAME = audits.AUDIT_NAME,
                    @AUDIT_DATE = audits.AUDIT_DATE,
                    @AUDIT_TIME = audits.AUDIT_TIME,
                    @AUDIT_SUBJECT = audits.AUDIT_SUBJECT,
                    @NUMBER_DAYS = audits.NUMBER_DAYS,
                    @KIND_AUDIT = audits.KIND_AUDIT,
                    @SCOPE_AUDIT = audits.SCOPE_AUDIT,
                    @AUDIT_PROCESS = audits.AUDIT_PROCESS,
                    @AUDIT_RULE = audits.AUDIT_RULE
                },
                commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingAudit(DeleteAudit);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private AUDITS MappingAudit(IEnumerable<AUDITS> AuditList)
        {
            AUDITS audit = new AUDITS();
            foreach (var item in AuditList)
            {
                audit.IDAUDITS = item.IDAUDITS;
                audit.AUDIT_NAME = item.AUDIT_NAME;
                audit.AUDIT_DATE = item.AUDIT_DATE;
                audit.AUDIT_TIME = item.AUDIT_TIME;
                audit.NUMBER_DAYS = item.NUMBER_DAYS;
                audit.AUDIT_SUBJECT = item.AUDIT_SUBJECT;
                audit.KIND_AUDIT   = item.KIND_AUDIT;
                audit.SCOPE_AUDIT = item.SCOPE_AUDIT;
                audit.AUDIT_PROCESS = item.AUDIT_PROCESS;
                audit.AUDIT_RULE = item.AUDIT_RULE;
                audit.CREATEDATE = item.CREATEDATE;
                audit.UPDATEDATE = item.UPDATEDATE;
            }
            return audit;
        }
    }
}
