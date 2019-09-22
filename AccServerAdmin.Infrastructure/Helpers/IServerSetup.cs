using AccServerAdmin.Domain;

namespace AccServerAdmin.Infrastructure.Helpers
{
    /// <summary>
    /// Interface that defines the server setup process
    /// </summary>
    public interface IServerSetup
    {
        /// <summary>
        /// Executes the server setup process
        /// </summary>
        /// <param name="server">Server to setup</param>
        void Execute(Server server);
    }
}
