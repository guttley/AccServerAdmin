using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// Config data for this admin application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AppSettings : IKeyedEntity
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Path from where each instance server layout will be copied
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string ServerBasePath { get; set; }

        /// <summary>
        /// Base path to where each instance server will be created
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string InstanceBasePath { get; set; }

        /// <summary>
        /// Admin passphrase
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string AdminPassphrase { get; set; }
    }
}
