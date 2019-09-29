using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// Unique Id of the server instance
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the server
        /// </summary>
        public string Name { get; set; }
    }
}
