using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccServerAdmin.Domain
{
    public class Session : IKeyedEntity
    {

        public Session()
        {
        }

        /// <summary>
        /// Unique Id of the server instance
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ImportFile { get; set; }

        public DateTime SessionTimestamp { get; set; }

        public string SessionType { get; set; }

        public string Track { get; set; }

        public bool IsWet { get; set; }
    }
}
