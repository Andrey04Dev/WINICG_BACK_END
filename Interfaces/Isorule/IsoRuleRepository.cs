using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Isorule
{
    public class IsoRuleRepository : IIsoRuleRepository
    {
        private readonly DbConnection db;

        public IsoRuleRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<ISORULE> AddIsoRule(ISORULE rule)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addRule = await conn.QueryAsync<ISORULE>("ISO.SP_ADD_ISORULE",
                    new
                    {
                        @IDAUDIT = rule.IDAUDIT,
                        @IDCERTIFICATION = rule.IDCERTIFICATION,
                        @IDNAMERULE = rule.NAMERULE,
                        @IDCODERULE = rule.CODERULE,
                        @RULE_DESCRIPTION = rule.RULE_DESCRIPTION
                    }, 
                    commandType: CommandType.StoredProcedure );
                var result = MappingIsorule(addRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<ISORULE>> GetAllIsoRule()
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var getAllIsorule = await conn.QueryAsync<ISORULE>("ISO.SP_GET_ALL_ISORULE", commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<ISORULE>)getAllIsorule;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ISORULE> GetIsoRuleById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getIsoruleById = await conn.QueryAsync<ISORULE>("ISO.SP_GET_ISORULE_BY_ID", new {@IDRULE = id}, commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingIsorule(getIsoruleById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ISORULE> RemoveIsoRule(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeIsoRule = await conn.QueryAsync<ISORULE>("ISO.SP_DELETE_ISORULE", new { @IDRULE= id}, commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingIsorule(removeIsoRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ISORULE> UpdateIsoRule(ISORULE rule, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateRule = await conn.QueryAsync<ISORULE>("ISO.SP_UPDATE_ISORULE", new
                {
                    @IDAUDIT = rule.IDAUDIT,
                    @IDCERTIFICATION = rule.IDCERTIFICATION,
                    @IDNAMERULE = rule.NAMERULE,
                    @IDCODERULE = rule.CODERULE,
                    @RULE_DESCRIPTION = rule.RULE_DESCRIPTION
                },
                    commandType: CommandType.StoredProcedure);
                var result = MappingIsorule(updateRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private ISORULE MappingIsorule(IEnumerable<ISORULE> isoRuleList)
        {
            ISORULE isorule = new ISORULE();
            foreach (var item in isoRuleList)
            {
                isorule.IDISORULE = item.IDISORULE;
                isorule.IDCERTIFICATION = item.IDCERTIFICATION;
                isorule.IDAUDIT = item.IDAUDIT;
                isorule.NAMERULE = item.NAMERULE;
                isorule.CODERULE = item.CODERULE;
                isorule.RULE_DESCRIPTION = item.RULE_DESCRIPTION;   
                isorule.CREATEDATE = item.CREATEDATE;
                isorule.UPDATEDATE = item.UPDATEDATE;
                isorule.AUDITS = item.AUDITS;
                isorule.CERTIFICATION = item.CERTIFICATION;
            }
            return isorule;
        }
    }
}
