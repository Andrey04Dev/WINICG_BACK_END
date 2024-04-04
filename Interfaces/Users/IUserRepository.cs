using webapi.Models;

namespace webapi.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<USERS> AddUsers(USERS users, string password);
        Task<USERS> RemoveUsers(string id);
        Task<USERS> UpdateUsers(USERS users, string password, string id);
        Task<List<USERS>> GetAllUsers();
        Task<USERS> GetUsersById(string id);
        Task <USERS> Login(string email, string password);
        Task<int> GetCountUser();
    }
}
