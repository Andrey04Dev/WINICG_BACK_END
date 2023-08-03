using webapi.Models;

namespace webapi.Interfaces.Certification
{
    public interface ICertificationRepository
    {
        Task<CERTIFICATION> AddCertification(CERTIFICATION certification);
        Task<CERTIFICATION> RemoveCertification(string id);
        Task<CERTIFICATION> UpdateCertification(CERTIFICATION certification, string id);
        Task<List<CERTIFICATION>> GetAllCertification();
        Task<CERTIFICATION> GetCertificationById(string id);
    }
}
