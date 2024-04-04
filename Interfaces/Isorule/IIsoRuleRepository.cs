using webapi.Models;

namespace webapi.Interfaces.Isorule
{
    public interface IIsoRuleRepository
    {
        Task<ISORULE> AddIsoRule(ISORULE rule);
        Task<ISORULE> RemoveIsoRule(string id);
        Task<ISORULE> UpdateIsoRule(ISORULE rule, string id);
        Task<List<ISORULE>> GetAllIsoRule();
        Task<ISORULE> GetIsoRuleById(string id);
        Task<int> GetCountIsoRule();
    }
}
