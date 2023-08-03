using webapi.Models;

namespace webapi.Interfaces.Process
{
    public interface IProcessRepository
    {
        Task<PROCESS> AddProcess(PROCESS process);
        Task<PROCESS> RemoveProcess(string id);
        Task<PROCESS> UpdateProcess(PROCESS process, string id);
        Task<List<PROCESS>> GetAllProcess();
        Task<PROCESS> GetProcessById(string id);
    }
}
