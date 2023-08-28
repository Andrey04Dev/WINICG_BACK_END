using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<USERS> AddUsers(USERS users, string password)
        {
            try
            {
                byte[]? passwordHash, passwordSalt;
                using var conn =  db.GetConnection();
                conn.Open();

                if (users == null || password == "") return null;

                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                users.PASSWORDHASH = passwordHash;
                users.PASSWORDSALT = passwordSalt;

                var addUser = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_ADD_USER",
                    map: (user, role) => { user.ROLE = role; return user; },
                    new {
                        @IDROLE = users.IDROLE,
                        @IDPOSITION = users.IDPOSITION,
                        @CEDULA = users.CEDULA,
                        @FULLNAME = users.FULLNAME,
                        @EMAIL = users.EMAIL,
                        @PASSWORDHASH = passwordHash, 
                        @PASSWORDSALT = passwordSalt
                    }, 
                    splitOn:"IDROLE",
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
                var getAllUsers = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_GET_ALL_USERS", map: (user, role) => { user.ROLE = role; return user; },
                    splitOn: "IDROLE",
                    commandType: System.Data.CommandType.StoredProcedure);
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
                var getUserByID = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_GET_USER_BY_ID", map: (user, role) => { user.ROLE = role; return user; },
                    new { @IDUSER = id},
                    splitOn:"IDROLE",
                    commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<USERS> Login(string email, string password)
        {
            IEnumerable<USERS> userFound = null;

            using var conn = db.GetConnection();
            conn.Open();
            userFound = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_LOGIN_USERS",
            map: (user, role) => { user.ROLE = role; return user; },
             param: new
             {
                 @EMAIL = email
             }
            , splitOn: "IDROLE",
            commandType: CommandType.StoredProcedure);

            if (userFound == null) return null;

            var resultUser = MappingUsers(userFound);

            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Email, ClaimTypes.Role);
            if (identity != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, resultUser.EMAIL));
                identity.AddClaim(new Claim(ClaimTypes.Role, resultUser.ROLE.ROLE));

            }

            if (!VerifyPasswordHashed(password, resultUser.PASSWORDHASH, resultUser.PASSWORDSALT)) return null;

            conn.Dispose();
            conn.Close();

            return resultUser;
        }

        public async Task<USERS> RemoveUsers(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeUser = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_REMOVE_USER",
                    map: (user, role) => { user.ROLE = role; return user; },
                    new { @IDUSER = id }, 
                    splitOn: "IDROLE",
                    commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<USERS> UpdateUsers(USERS users,string password, string id)
        {
            try
            {
                byte[]? passwordHash, passwordSalt;
                using var conn = db.GetConnection();
                conn.Open();


                if (users == null || id == "") return null;

                if (password != null)
                {
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    users.PASSWORDHASH = passwordHash;
                    users.PASSWORDSALT = passwordSalt;
                }

                var updateUser = await conn.QueryAsync<USERS, ROLES, USERS>("US.SP_UPDATE_USER",
                    map: (user, role) => { user.ROLE = role; return user; },
                    new
                    {
                        @IDUSER = id,
                        @IDROLE = users.IDROLE,
                        @IDPOSITION = users.IDPOSITION,
                        @CEDULA = users.CEDULA,
                        @FULLNAME = users.FULLNAME,
                        @EMAIL = users.EMAIL,
                        @PASSWORDHASH = users.PASSWORDHASH,
                        @PASSWORDSALT = users.PASSWORDSALT, 
                        @ACTIVE =  users.ACTIVE
                    },
                    splitOn:"IDROLE",
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
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
                users.IDPOSITION = item.IDPOSITION;
                users.FULLNAME = item.FULLNAME;
                users.CEDULA = item.CEDULA;
                users.EMAIL = item.EMAIL;
                users.PASSWORDHASH = item.PASSWORDHASH;
                users.PASSWORDSALT = item.PASSWORDSALT;
                users.ACTIVE = item.ACTIVE;
                users.CREATEDATE = item.CREATEDATE;
                users.UPDATEDATE = item.UPDATEDATE;
                users.ROLE = item.ROLE;
            }
            return users;
        }

        private bool VerifyPasswordHashed(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computerHash.Length; i++)
                {
                    if (computerHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passswordSalt = hmac.Key;
            }
        }

        private static string DecrypingPassword(byte[] bytes)
        {
            using (MemoryStream Stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(Stream))
                {
                    var resul = streamReader.ReadToEnd();
                    return Convert.FromBase64String(resul).ToString();
                }
            }

        }
    }
}
