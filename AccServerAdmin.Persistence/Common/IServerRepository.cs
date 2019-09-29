using AccServerAdmin.Domain;
using System.Threading.Tasks;

namespace AccServerAdmin.Persistence.Common
{
    public interface IServerRepository : IDataRepository<Server>
    {
        /// <summary>
        /// Returns true if another server exists with the same name
        /// </summary>
        Task<bool> IsUniqueNameAsync(string serverName);
    }
}
