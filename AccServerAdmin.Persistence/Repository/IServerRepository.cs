using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Persistence.Repository
{
    public interface IServerRepository : IDataRepository<Server>
    {
        /// <summary>
        /// Returns true if another server exists with the same name
        /// </summary>
        Task<bool> IsUniqueNameAsync(string serverName);

        /// <summary>
        /// Returns true if the ports are unique to the server
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="tcpPort"></param>
        /// <param name="udpPort"></param>
        /// <returns></returns>
        Task<bool> IsDuplicatePortsAsync(Guid serverId, int tcpPort, int udpPort);
    }
}
