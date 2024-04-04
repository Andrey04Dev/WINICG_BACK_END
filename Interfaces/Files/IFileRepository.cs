using webapi.Models;

namespace webapi.Interfaces.Files
{
    public interface IFileRepository
    {
        Task<IEnumerable<FILES>> ListFilesByID(string id);
        Task<List<FILES>> AddFiles(List<FILES> files,string id);
        Task<FILES> removeImage(string idModule, string idFile);
        Task<int> GetCountFiles();
    }
}
