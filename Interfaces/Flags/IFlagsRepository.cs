using webapi.Models;

namespace webapi.Interfaces.Flags
{
    public interface IFlagsRepository
    {
        Task<FLAGS> AddFlags(FLAGS flag);
        Task<FLAGS> RemoveFlags(string id);
        Task<FLAGS> UpdateFlags(FLAGS flag, string id);
        Task<List<FLAGS>> GetAllFlags();
        Task<FLAGS> GetFlagsById(string id);
    }
}
