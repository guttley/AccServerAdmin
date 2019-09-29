using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// Config data for this admin application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppSettings
    {
        /// <summary>
        /// Path from where each instance server layout will be copied
        /// </summary>
        public string ServerBasePath { get; set; }

        /// <summary>
        /// Base path to where each instance server will be created
        /// </summary>
        public string InstanceBasePath { get; set; }
    }
}
