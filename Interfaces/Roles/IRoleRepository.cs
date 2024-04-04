using webapi.DTO.Audits;
using webapi.Models;

namespace webapi.Interfaces.Roles
{
    public interface IRoleRepository
    {
        Task<ROLES> AddRoles(ROLES roles);
        Task<ROLES> RemoveRoles(string id);
        Task<ROLES> UpdateRoles(ROLES roles, string id);
        Task<List<ROLES>> GetAllRoles();
        Task<ROLES> GetRolesById(string id);
        Task<int> GetCountRoles();
    }
}
