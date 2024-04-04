using Dapper;
using System.Data;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Historial
{
    public class HistorialRepository : IHistorialRepository
    {
        private readonly DbConnection db;

        public HistorialRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<HISTORIAL> CreateHistorialAsync(HISTORIAL historial)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var addHistory = await conn.QueryAsync<HISTORIAL>("HIS.SP_ADD_HISTORY",
                new
                {
                        @IDMODULE = historial.IDMODULE,
                        @PERSONCHANGE=historial.PERSONCHANGE,
                },
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingHistorial(addHistory);
                if (result == null)
                {
                    return result;
                }
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<HISTORIAL> GetHistorialByIdModule(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getHistoryById = await conn.QueryAsync<HISTORIAL>("HIS.SP_GET_HISTORIAL_BY_ID", new { @IDMODULE = id }, commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingHistorial(getHistoryById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private HISTORIAL MappingHistorial(IEnumerable<HISTORIAL> historyList)
        {
            HISTORIAL history = new HISTORIAL();
            foreach (var item in historyList)
            {
                history.IDHISTORIAL = item.IDHISTORIAL;
                history.IDMODULE = item.IDMODULE;
                history.PERSONCHANGE = item.PERSONCHANGE;
                history.DATEHISTORY = item.DATEHISTORY;
                history.HOUR =  item.HOUR;
            }
            return history;
        }
    }
}
