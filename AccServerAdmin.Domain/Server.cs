using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// This class holds the data for the server instance
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Server : IKeyedEntity
    {
        /// <summary>
        /// Unique Id of the server instance
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the server
        /// </summary>
        public string Name
        {
            get => GameConfiguration.ServerName;
            set => GameConfiguration.ServerName = value;
        }

        /// <summary>
        /// Network settings
        /// </summary>
        public NetworkConfiguration NetworkConfiguration { get; set; } = new NetworkConfiguration();

        /// <summary>
        /// Game settings
        /// </summary>
        public GameConfiguration GameConfiguration { get; set; } = new GameConfiguration();

        /// <summary>
        /// Event settings
        /// </summary>
        public EventConfiguration EventConfiguration { get; set; } = new EventConfiguration();


    }
}
