using webapi.Models;

namespace webapi.Interfaces.Historial
{
    public interface IHistorialRepository
    {
        Task<HISTORIAL> CreateHistorialAsync(HISTORIAL historial);
        Task<HISTORIAL> GetHistorialByIdModule(string id);
    }
}
