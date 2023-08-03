using webapi.Models;

namespace webapi.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<USERS> AddUsers(USERS users);
        Task<USERS> RemoveUsers(string id);
        Task<USERS> UpdateUsers(USERS users, string id);
        Task<List<USERS>> GetAllUsers();
        Task<USERS> GetUsersById(string id);
    }
}
