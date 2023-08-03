using webapi.Models;

namespace webapi.Interfaces.Risks
{
    public interface IRisksRepository
    {
        Task<RISKS> AddRisks(RISKS risks);
        Task<RISKS> RemoveRisks(string id);
        Task<RISKS> UpdateRisks(RISKS risks, string id);
        Task<List<RISKS>> GetAllRisks();
        Task<RISKS> GetRisksById(string id);
    }
}
