using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccServerAdmin.Domain
{
    public class Session : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime SessionTimestamp { get; set; }

        public string SessionType { get; set; }

        public string Track { get; set; }

        public bool IsWet { get; set; }

        public List<SessionLap> Laps { get; set; } = new List<SessionLap>();
    }
}
