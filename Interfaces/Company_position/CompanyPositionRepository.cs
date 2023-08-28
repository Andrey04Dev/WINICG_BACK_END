using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Interfaces.Company_position
{
    public class CompanyPositionRepository : ICompanyPositionRepository
    {
        private readonly DbConnection db;

        public CompanyPositionRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<COMPANY_POSITION> AddCompanyPosition(COMPANY_POSITION company)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var addPosition = await conn.QueryAsync<COMPANY_POSITION>("ORG.SP_ADD_COMPANY_POSITION",
                    new {
                        @IDUSER=company.IDUSER,
                        @IDPROCESS=company.IDPROCESS,
                        @MANDATED=company.MANDATED,
                        @DESCRIPTION=company.DESCRIPTION,
                        @RESPONSABILITIES=company.RESPONSABILITIES,
                        @PROFILE_POSITION=company.PROFILE_POSITION,
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingCompanyPosition(addPosition);
                    return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<COMPANY_POSITION>> GetAllCompanyPosition()
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var getCompanyPosition = await  conn.QueryAsync<COMPANY_POSITION,USERS, PROCESS, COMPANY_POSITION>("ORG.SP_GET_ALL_COMPANY_POSITION",
                    map: (company,user, process) => { company.USERS = user; company.PROCESS = process; return company; },
                    splitOn: "IDUSER,IDPROCESS",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<COMPANY_POSITION>)getCompanyPosition;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<COMPANY_POSITION> GetCompanyPositionByID(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var GetPositionById = await conn.QueryAsync<COMPANY_POSITION, USERS, PROCESS, COMPANY_POSITION>("ORG.SP_GET_POSITION_BY_ID",
                    map: (company, user, process) => { company.USERS = user; company.PROCESS = process; return company; },
                    new { @IDCOMPANYPOSITION =id },
                    splitOn: "IDUSER,IDPROCESS",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingCompanyPosition(GetPositionById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<COMPANY_POSITION> RemoveCompanyPosition(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var RemovePosition = await conn.QueryAsync<COMPANY_POSITION, USERS, PROCESS, COMPANY_POSITION>("ORG.SP_REMOVE_COMPANY_POSITION",
                    map: (company, user, process) => { company.USERS = user; company.PROCESS = process; return company; },
                    new { @IDCOMPANYPOSITION = id },
                    splitOn: "IDUSER,IDPROCESS",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingCompanyPosition(RemovePosition);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<COMPANY_POSITION> UpdateCompanyPosition(COMPANY_POSITION company, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var UpdatePostion = await conn.QueryAsync<COMPANY_POSITION, USERS, PROCESS, COMPANY_POSITION>($"ORG.SP_UPDATE_COMPANY_POSITION",
                    map: (company, user, process) => { company.USERS = user; company.PROCESS = process; return company; },
                    new
                {
                        @IDCOMPANY_POSITION=id,
                        @IDUSER = company.IDUSER,
                        @IDPROCESS = company.IDPROCESS,
                        @MANDATED = company.MANDATED,
                        @DESCRIPTION = company.DESCRIPTION,
                        @RESPONSABILITIES = company.RESPONSABILITIES,
                        @PROFILE_POSITION = company.PROFILE_POSITION,
                    },
                    splitOn: "IDUSER,IDPROCESS",
                    commandType: System.Data.CommandType.StoredProcedure);
                    conn.Close();
                conn.Dispose();
                var result = MappingCompanyPosition(UpdatePostion);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private COMPANY_POSITION MappingCompanyPosition(IEnumerable<COMPANY_POSITION> companyPositionList)
        {
            COMPANY_POSITION company = new COMPANY_POSITION();
            foreach (var item in companyPositionList)
            {
                company.IDCOMPANY_POSITION = item.IDCOMPANY_POSITION;
                company.IDUSER = item.IDUSER;
                company.IDPROCESS = item.IDPROCESS;
                company.MANDATED = item.MANDATED;   
                company.PROFILE_POSITION = item.PROFILE_POSITION;
                company.DESCRIPTION = item.DESCRIPTION;
                company.RESPONSABILITIES = item.RESPONSABILITIES;
                company.CREATEDATE = item.CREATEDATE;
                company.UPDATEDATE = item.UPDATEDATE;
                company.USERS = item.USERS;
                company.PROCESS = item.PROCESS;
            }
            return company;
        }
    }
}
