using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.DTO.Audits;
using webapi.DTO.Roles;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Interfaces.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbConnection db;

        public RoleRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<ROLES> AddRoles(ROLES roles)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addRole = await conn.QueryAsync<ROLES>("US.ADD_ROLE",
                    new {
                        @ROLE=roles.ROLE
                    }, 
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingRoles(addRole);
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

        public async Task<List<ROLES>> GetAllRoles()
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getRoles = await conn.QueryAsync<ROLES>("US.GET_ALL_ROLE", commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return getRoles.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> GetCountRoles()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM US.ROLES";
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

        public async Task<ROLES> GetRolesById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getRolesById = await conn.QueryAsync<ROLES>("US.GET_ROLES_BY_ID", new { @IDROLE = id }, commandType:CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingRoles(getRolesById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ROLES> RemoveRoles(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeRoles = await  conn.QueryAsync<ROLES>("US.DELETE_ROLE", new{@IDROLE = id}, commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingRoles((IEnumerable<ROLES>)removeRoles);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ROLES> UpdateRoles(ROLES roles, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateRole = await conn.QueryAsync<ROLES>("US.UPDATE_ROLE",
                    new {
                        @ROLE = roles.ROLE,
                        @IDROLE= id
                    }, 
                   commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingRoles((IEnumerable<ROLES>)updateRole);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private ROLES MappingRoles(IEnumerable<ROLES> rolesList)
        {
            ROLES roles = new ROLES();
            foreach (var item in rolesList)
            {
                roles.IDROLE = item.IDROLE;
                roles.ROLE = item.ROLE;
                roles.PERSONCHANGE =  item.PERSONCHANGE;
                roles.CREATEDATE = item.CREATEDATE;
                roles.UPDATEDATE = item.UPDATEDATE;
            }
            return roles;
        }
    }
}
