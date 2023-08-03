using webapi.Models;

namespace webapi.Interfaces.No_Accordance
{
    public interface INoAccordanceRepository
    {
        Task<NO_ACCORDANCE> AddNoAccordance(NO_ACCORDANCE accordance);
        Task<NO_ACCORDANCE> RemoveNoAccordance(string id);
        Task<NO_ACCORDANCE> UpdateNoAccordance(NO_ACCORDANCE accordance, string id);
        Task<List<NO_ACCORDANCE>> GetAllNoAccordance();
        Task<NO_ACCORDANCE> GetNoAccordanceById(string id);
    }
}
