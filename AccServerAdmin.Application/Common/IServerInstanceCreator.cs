using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface IServerInstanceCreator
    {
        /// <summary>
        /// Executes the server instance creation process
        /// </summary>
        /// <param name="server">Server</param>
        /// <returns>Path to the server instance</returns>
        Task<string> Execute(Server server);
    }
}
