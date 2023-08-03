using webapi.Models;

namespace webapi.Interfaces.Company_position
{
    public interface ICompanyPositionRepository
    {
        Task<COMPANY_POSITION> AddCompanyPosition(COMPANY_POSITION company);
        Task<COMPANY_POSITION> RemoveCompanyPosition(string id);
        Task<COMPANY_POSITION> UpdateCompanyPosition(COMPANY_POSITION company, string id);
        Task<List<COMPANY_POSITION>> GetAllCompanyPosition();
        Task<COMPANY_POSITION> GetCompanyPositionByID(string id);
    }
}
