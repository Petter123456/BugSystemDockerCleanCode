using BugsFrontend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugsFrontend.Interfaces
{
    public interface IBugsApiRequest
    {
        Task<List<BugModel>> GetBugsAsync();
        Task DeleteBugAsync(int id);
        Task UpdateBugAsync(int id, string name);
        Task CreateBugAsync(string name);
    }
}