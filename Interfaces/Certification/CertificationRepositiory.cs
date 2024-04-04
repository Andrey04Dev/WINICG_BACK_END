using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Interfaces.Certification
{
    public class CertificationRepositiory : ICertificationRepository
    {
        private readonly DbConnection db;

        public CertificationRepositiory(DbConnection db)
        {
            this.db = db;
        }
        public async Task<CERTIFICATION> AddCertification(CERTIFICATION certification)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                if (certification == null) throw new ArgumentNullException("The certification is empty!");
                var addCertification = await conn.QueryAsync<CERTIFICATION>("ISO.SP_ADD_CERTIFICATION",
                    new {
                        @CERTIFICATION_NAME = certification.CERTIFICATION_NAME,
                        @CERTIFICATION_DATE = certification.CERTIFICACION_DATE
                    }, 
                commandType: CommandType.StoredProcedure);

                conn.Close();
                conn.Dispose();
                var result = MappingCertification(addCertification);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<CERTIFICATION>> GetAllCertification()
        {
             try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllCertification = await conn.QueryAsync<CERTIFICATION>("ISO.SP_GET_ALL_CERTIFICATION", commandType: CommandType.StoredProcedure);
                conn.Close();   
                conn.Dispose();

                return getAllCertification.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<CERTIFICATION> GetCertificationById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getCertificationById = await conn.QueryAsync<CERTIFICATION>("ISO.SP_GET_CERTIFICATION_BY_ID",
                    new {
                    @IDCERTIFICATION = id
                    }, 
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingCertification(getCertificationById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> GetCountCertification()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM ISO.CERTIFICATION";
            var GetCertification = await conn.QueryAsync<int>(sql);
            conn.Close();
            conn.Dispose();
            var result = 0;
            foreach (var audit in GetCertification)
            {
                result = audit;
            }
            return result;
        }

        public async Task<CERTIFICATION> RemoveCertification(string id)
        {
            try
            {
                using var conn = db.GetConnection();    
                conn.Open();
                var removeCertification = await conn.QueryAsync<CERTIFICATION>("ISO.SP_REMOVE_CERTIFICATION",
                    new { 
                    @IDCERTIFICATION = id
                    }, 
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingCertification(removeCertification);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<CERTIFICATION> UpdateCertification(CERTIFICATION certification, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateCertification = await conn.QueryAsync<CERTIFICATION>("ISO.SP_UPDATE_CERTIFICATION ", 
                    new
                    {
                        @CERTIFICATION_NAME = certification.CERTIFICATION_NAME,
                        @CERTIFICATION_DATE = certification.CERTIFICACION_DATE, 
                        @IDCERTIFICATION = id
                    }, 
                commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                    
                var result = MappingCertification(updateCertification);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private CERTIFICATION MappingCertification(IEnumerable<CERTIFICATION> certificationList)
        {
            CERTIFICATION certification = new CERTIFICATION();
            foreach (var item in certificationList)
            {
                certification.IDCERTIFICATION = item.IDCERTIFICATION;
                certification.CERTIFICACION_DATE = item.CERTIFICACION_DATE;
                certification.CERTIFICATION_NAME = item.CERTIFICATION_NAME;
                certification.PERSONCHANGE = item.PERSONCHANGE;
                certification.CREATEDATE = item.CREATEDATE;
                certification.UPDATEDATE = item.UPDATEDATE;
            }
            return certification;
        }
    }
}
