using AccServerAdmin.Domain;

namespace AccServerAdmin.Persistence.Common
{

    /// <summary>
    /// Interface to persist configuration information
    /// </summary>
    public interface IAppSettingsRepository
    {
        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <param name="config">Configuration to save</param>
        void Save(AppSettings config);

        /// <summary>
        /// Reads the configuration
        /// </summary>
        /// <param name="createIfNotExists">If true will return a default object, else null</param>
        AppSettings Read(bool createIfNotExists = true);

    }
}
