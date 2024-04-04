using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Interfaces.Risks
{
    public class RisksRepository : IRisksRepository
    {
        private readonly DbConnection db;

        public RisksRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<RISKS> AddRisks(RISKS risks)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addRisk = await conn.QueryAsync<RISKS,ISORULE, RISKS>("ISO.SP_ADD_RISK",
                    map: (risk, rule) => { risk.ISORULE = rule; return risk; },
                    new {
                        @IDRULE = risks.IDRULE,
                        @NAMERISKS =risks.NAMERISKS,
                        @ORIGEN =  risks.ORIGEN,
                        @CONSEQUENSE=risks.CONSEQUENSE,
                        @SOURCE_RISK= risks.SOURCE_RISK,
                        @STATE =  risks.STATE,
                    },
                    splitOn:"IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingRisks(addRisk);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<RISKS>> GetAllRisks()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllRisks = await conn.QueryAsync<RISKS, ISORULE, RISKS>("ISO.SP_GET_ALL_RISK",
                    map: (risk, rule) => { risk.ISORULE = rule; return risk; }, 
                    splitOn:"IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return getAllRisks.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> GetCountRisks()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM ISO.RISKS";
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

        public async Task<RISKS> GetRisksById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getRiskById =  await conn.QueryAsync<RISKS, ISORULE, RISKS>("ISO.SP_GET_RISK_BY_ID",
                    map: (risk, rule) => { risk.ISORULE = rule; return risk; }
                    , new
                {
                    @IDRISKS = id
                }, 
                    splitOn:"IDRULE",
                commandType: System.Data.CommandType.StoredProcedure);
                var result =  MappingRisks(getRiskById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RISKS> RemoveRisks(string id)
        {
            using var conn =  db.GetConnection();   
            conn.Open();
            var removeRisks = await conn.QueryAsync<RISKS, ISORULE, RISKS>("ISO.SP_REMOVE_RISK", map: (risk, rule) => { risk.ISORULE = rule; return risk; },
                new {@IDRISKS =  id},
                splitOn:"IDRULE",
                commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose();
            var result = MappingRisks(removeRisks);
            return result;
        }

        public async Task<RISKS> UpdateRisks(RISKS risks, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateRisk = await conn.QueryAsync<RISKS, ISORULE, RISKS>($"ISO.SP_UPDATE_RISK",
                    map: (risk, rule) => { risk.ISORULE = rule; return risk; },
                    new
                    {
                        @IDRISKS = id,
                        @IDRULE = risks.IDRULE,
                        @NAMERISKS = risks.NAMERISKS,
                        @ORIGEN = risks.ORIGEN,
                        @CONSEQUENSE = risks.CONSEQUENSE,
                        @SOURCE_RISK = risks.SOURCE_RISK,
                        @STATE =  risks.STATE,
                    },
                    splitOn:"IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingRisks(updateRisk);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private RISKS MappingRisks(IEnumerable<RISKS> RisksList)
        {
            RISKS risk = new RISKS();
            foreach (var item in RisksList)
            {
                risk.IDRISKS = item.IDRISKS;
                risk.IDRULE = item.IDRULE;
                risk.NAMERISKS = item.NAMERISKS;
                risk.ORIGEN = item.ORIGEN;
                risk.CONSEQUENSE = item.CONSEQUENSE;
                risk.SOURCE_RISK = item.SOURCE_RISK;
                risk.STATE = item.STATE;
                risk.QUANTITY = item.QUANTITY;
                risk.CREATEDATE = item.CREATEDATE;
                risk.UPDATEDATE = item.UPDATEDATE;
                risk.ISORULE = item.ISORULE;
            }
            return risk;
        }
    }
}
