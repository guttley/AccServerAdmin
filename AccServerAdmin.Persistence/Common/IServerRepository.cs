using System;
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

        /// <summary>
        /// Returns true if the ports are unique to the server
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="tcpPort"></param>
        /// <param name="udpPort"></param>
        /// <returns></returns>
        Task<bool> IsDuplicatePortsAsync(Guid serverId, in int tcpPort, in int udpPort);
    }
}
