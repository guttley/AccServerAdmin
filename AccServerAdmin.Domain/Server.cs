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
        private string _name;

        public Server()
        {
            NetworkCfg = NetworkCfg.CreateDefault();
            GameCfg = GameCfg.CreateDefault();
            EventCfg = EventCfg.CreateDefault();
        }

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
            get => _name;
            set
            {
                _name = value;
                GameCfg.ServerName = value;
            }
        }

        /// <summary>
        /// Network settings
        /// </summary>
        public NetworkCfg NetworkCfg { get; set; } 

        /// <summary>
        /// Game settings
        /// </summary>
        public GameCfg GameCfg { get; set; }

        /// <summary>
        /// Event settings
        /// </summary>
        public EventCfg EventCfg { get; set; } 


    }
}
