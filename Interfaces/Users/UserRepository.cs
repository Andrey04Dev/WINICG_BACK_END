using Dapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Interfaces.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection db;

        public UserRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<USERS> AddUsers(USERS users)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addUser = await conn.QueryAsync<USERS>("US.SP_ADD_USER", 
                    new {
                        @IDROLE = users.IDROLE,
                        @ID=users.ID,
                        @EMAIL = users.EMAIL}, 
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingUsers(addUser);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<USERS>> GetAllUsers()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getAllUsers = await conn.QueryAsync<USERS>("US.SP_GET_ALL_USERS", commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return getAllUsers.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<USERS> GetUsersById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getUserByID = await conn.QueryAsync<USERS>($"US.SP_GET_USER_BY_ID", new { @IDUSER = id}, commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingUsers(getUserByID);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<USERS> RemoveUsers(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeUser = await conn.QueryAsync<USERS>($"US.SP_REMOVE_USER", new { @IDUSER = id }, commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingUsers(removeUser);  
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<USERS> UpdateUsers(USERS users, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateUser = await conn.QueryAsync<USERS>("US.SP_UPDATE_USER",
                    new
                    {
                        @IDROLE = users.IDROLE,
                        @ID = users.ID,
                        @EMAIL = users.EMAIL
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                var result = MappingUsers(updateUser);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private USERS MappingUsers(IEnumerable<USERS> certificationList)
        {
            USERS users = new USERS();
            foreach (var item in certificationList)
            {
                users.IDUSER = item.IDUSER;
                users.IDROLE = item.IDROLE;
                users.ID = item.ID;
                users.EMAIL = item.EMAIL;
                users.CREATEDATE = item.CREATEDATE;
                users.UPDATEDATE = item.UPDATEDATE;
            }
            return users;
        }
    }
}
