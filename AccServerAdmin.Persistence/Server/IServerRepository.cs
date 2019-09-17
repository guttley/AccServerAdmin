namespace AccServerAdmin.Persistence.Server
{
    using AccServerAdmin.Domain;

    /// <summary>
    /// Interface to persist server information to disk
    /// </summary>
    public interface IServerRepository
    {
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
