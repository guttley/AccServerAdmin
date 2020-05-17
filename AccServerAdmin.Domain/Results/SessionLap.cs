using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain.Results
{
    public class SessionLap : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public SessionCar Car { get; set; }

        public Driver Driver { get; set; }

        public int Lap { get; set; }

        public bool Valid { get; set; }

        public long LapTime { get; set; }

        public long Split1 { get; set; }

        public long Split2 { get; set; }

        public long Split3 { get; set; }

    }
}
