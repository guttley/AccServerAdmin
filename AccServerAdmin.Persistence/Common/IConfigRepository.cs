namespace AccServerAdmin.Persistence.Common
{

    /// <summary>
    /// Interface to persist configuration information
    /// </summary>
    public interface IConfigRepository<T> where T : new()
    {
        /// <summary>
        /// Returns a new configuration
        /// </summary>
        T New();

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <param name="serverDirectory">Server instance path</param>
        /// <param name="config">Configuration to save</param>
        void Save(string serverDirectory, T config);

        /// <summary>
        /// Reads the configuration given the server instance base path
        /// </summary>
        /// <param name="serverDirectory">Server instance path</param>
        /// <returns></returns>
        T Read(string serverDirectory);

    }
}
