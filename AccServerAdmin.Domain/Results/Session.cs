using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccServerAdmin.Domain.Results
{
    public class Session : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ServerName { get; set; }

        public DateTime SessionTimestamp { get; set; }

        public string SessionType { get; set; }

        public string Track { get; set; }

        public bool IsWet { get; set; }

        public int BestLap { get; set; }

        public int BestSplit1 { get; set; }

        public int BestSplit2 { get; set; }

        public int BestSplit3 { get; set; }

        public List<SessionLap> Laps { get; set; } = new List<SessionLap>();

        public List<LeaderboardLine> LeaderBoard { get; set; } = new List<LeaderboardLine>();

        public List<SessionPenalty> Penalties { get; set; } = new List<SessionPenalty>();
    }
}
