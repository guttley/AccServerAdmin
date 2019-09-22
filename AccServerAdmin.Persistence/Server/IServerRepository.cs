namespace AccServerAdmin.Persistence.Server
{
    using Domain;

    /// <summary>
    /// Interface to persist server information
    /// </summary>
    public interface IServerRepository
    {
        /// <summary>
        /// Returns a new server
        /// </summary>
        /// <param name="serverName">Name of the new server</param>
        Server New(string serverName);

        /// <summary>
        /// Writes the server config to disk
        /// </summary>
        void Save(Server server);

        /// <summary>
        /// Returns a new server populated from the json config file for the server instance
        /// </summary>
        /// <param name="directory">Server instance directory</param>
        Server Read(string directory);
    }
}
