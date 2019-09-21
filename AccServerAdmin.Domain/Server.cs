using System;
using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// This class holds the data for the server instance
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Server
    {
        /// <summary>
        /// Filename of the config file
        /// </summary>
        public const string Filename = "AccAdmin.json";

        /// <summary>
        /// Unique Id of the server instance
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location of the server instance
        /// </summary>
        public string Location { get; set; }

    }
}
