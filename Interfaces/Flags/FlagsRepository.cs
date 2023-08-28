using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Flags
{
    public class FlagsRepository : IFlagsRepository
    {
        private readonly DbConnection db;

        public FlagsRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<FLAGS> AddFlags(FLAGS flag)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var addFlags = await conn.QueryAsync<FLAGS, ISORULE, FLAGS>("ISO.SP_ADD_FLAG", map: (flag, rule) => { flag.ISORULE = rule; return flag; },
                    new { @IDRULE = flag.IDRULE, @FLAGNAME = flag.FLAGNAME },
                    splitOn:"IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingFlags(addFlags);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<FLAGS>> GetAllFlags()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllFLags = await conn.QueryAsync<FLAGS, ISORULE, FLAGS>("ISO.SP_GET_ALL_FLAG", map: (flag, rule) => { flag.ISORULE = rule; return flag; },
                    splitOn: "IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<FLAGS>)getAllFLags;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<FLAGS> GetFlagsById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getFlagById = await conn.QueryAsync<FLAGS, ISORULE, FLAGS>("ISO.SP_GET_FLAG_BY_ID ", map: (flag, rule) => { flag.ISORULE = rule; return flag; },
                    new {@IDFLAGS = id},
                     splitOn: "IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingFlags(getFlagById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<FLAGS> RemoveFlags(string id)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var removeFlag = await conn.QueryAsync<FLAGS, ISORULE, FLAGS>("ISO.SP_REMOVE_FLAG", map: (flag, rule) => { flag.ISORULE = rule; return flag; },
                new { @IDFLAGS = id },splitOn: "IDRULE",commandType: System.Data.CommandType.StoredProcedure);
            var result = MappingFlags(removeFlag);
            return result;
        }

        public async Task<FLAGS> UpdateFlags(FLAGS flag, string id)
        {
            using var conn = db.GetConnection();
            var updateFlag= await conn.QueryAsync<FLAGS, ISORULE, FLAGS>("ISO.SP_UPDATE_FLAG ", map: (flag, rule) => { flag.ISORULE = rule; return flag; },
                new { @IDRULE = flag.IDRULE, @FLAGNAME = flag.FLAGNAME, @IDFLAGS= id },
                splitOn: "IDRULE",
                    commandType: System.Data.CommandType.StoredProcedure);
            var result = MappingFlags(updateFlag);
            return result;
        }
        private FLAGS MappingFlags(IEnumerable<FLAGS> FlagList)
        {
            FLAGS flag = new FLAGS();
            foreach (var item in FlagList)
            {
                flag.IDFLAGS = item.IDFLAGS;
                flag.IDRULE = item.IDRULE;
                flag.FLAGNAME = item.FLAGNAME;
                flag.CREATEDATE = item.CREATEDATE;
                flag.UPDATEDATE = item.UPDATEDATE;
            }
            return flag;
        }
    }
}
