using webapi.Models;

namespace webapi.Interfaces.Position
{
    public interface IPositionRepository
    {
        Task<POSITION> AddPosition(POSITION position);
        Task<POSITION> RemovePosition(string id);
        Task<POSITION> UpdatePosition(POSITION position, string id);
        Task<List<POSITION>> GetAllPosition();
        Task<POSITION> GetPositionById(string id);
        Task<int> GetCountPosition();
    }
}
