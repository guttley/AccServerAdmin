using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Persistence.ServerConfig
{

    /// <summary>
    /// Interface to persist server configuration information
    /// </summary>
    public interface IServerConfigRepository
    {
        /// <summary>
        /// Returns a new configuration
        /// </summary>
        Configuration New();

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <param name="serverDirectory">Server instance path</param>
        /// <param name="config">Configuration to save</param>
        void Save(string serverDirectory, Configuration config);

        /// <summary>
        /// Reads the configuration given the server instance base path
        /// </summary>
        /// <param name="serverDirectory">Server instance path</param>
        /// <returns></returns>
        Configuration Read(string serverDirectory);

    }
}
