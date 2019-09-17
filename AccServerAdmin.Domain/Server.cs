using System;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// This class holds the data for the server instance
    /// </summary>
    public class Server
    {
        public Server()
        {
            Id = Guid.NewGuid();
        }

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
