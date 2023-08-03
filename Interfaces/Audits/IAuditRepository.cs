using webapi.Models;

namespace webapi.Interfaces.Audits
{
    public interface IAuditRepository
    {
        Task<AUDITS> AddAudits(AUDITS audits);
        Task<AUDITS> RemoveAudits(string id);
        Task<AUDITS> UpdateAudits(AUDITS audits, string id);
        Task<List<AUDITS>> GetAllAudits();
        Task<AUDITS> GetAuditsById(string id);
    }
}
