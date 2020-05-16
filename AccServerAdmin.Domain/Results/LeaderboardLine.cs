using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain.Results
{
    public class LeaderboardLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public SessionCar Car { get; set; }

        public Driver CurrentDriver { get; set; }

        public int LastLap { get; set; }

        public int LastSplit1 { get; set; }

        public int LastSplit2 { get; set; }

        public int LastSplit3 { get; set; }

        public int BestLap { get; set; }

        public int BestSplit1 { get; set; }

        public int BestSplit2 { get; set; }

        public int BestSplit3 { get; set; }

        public int TotalTime { get; set; }

        public int LapCount { get; set; }

        public bool MissingMandatoryPitstop { get; set; }

    }
}


