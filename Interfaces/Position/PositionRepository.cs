using Dapper;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Position
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DbConnection db;

        public PositionRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<POSITION> AddPosition(POSITION position)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var addPostion = await conn.QueryAsync<POSITION>("US.SP_ADD_POSITION",
                    new
                    {
                        @POSITIONJOB = position.POSITIONJOB,
                        @DESCRIPTION = position.DESCRIPTION,
                        @AREA = position.AREA
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingPosition(addPostion);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<POSITION>> GetAllPosition()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllPosition = await conn.QueryAsync<POSITION>("US.SP_GET_ALL_POSITION",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<POSITION>)getAllPosition;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<POSITION> GetPositionById(string id)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var gePositionById = await conn.QueryAsync<POSITION>("US.SP_GET_POSITION_BY_ID",
                new { @IDPOSITION = id },
                commandType: System.Data.CommandType.StoredProcedure);
            var result = MappingPosition(gePositionById);
            return result;
        }

        public async Task<POSITION> RemovePosition(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removePosition = await conn.QueryAsync<POSITION>("US.SP_REMOVE_POSITION",
                    new { @IDPOSITION = id }
                , commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingPosition(removePosition);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<POSITION> UpdatePosition(POSITION position, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updatePosition = await conn.QueryAsync<POSITION>("US.SP_UPDATE_POSITION",
                    new
                    {
                        @IDPOSITION = id,
                        @POSITIONJOB = position.POSITIONJOB,
                        @DESCRIPTION = position.DESCRIPTION,
                        @AREA = position.AREA
                    },
                        commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingPosition(updatePosition);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private POSITION MappingPosition(IEnumerable<POSITION> PositionList)
        {
            POSITION position = new POSITION();
            foreach (var item in PositionList)
            {
                position.IDPOSITION = item.IDPOSITION;
                position.POSITIONJOB = item.POSITIONJOB;
                position.DESCRIPTION = item.DESCRIPTION;
                position.AREA = item.AREA;
                position.CREATEDATE = item.CREATEDATE;
                position.UPDATEDATE = item.UPDATEDATE;
            }
            return position;
        }
    }
}
