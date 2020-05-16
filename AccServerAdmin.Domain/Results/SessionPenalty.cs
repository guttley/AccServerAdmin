using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccServerAdmin.Domain.Results
{
    public class SessionPenalty : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SessionId { get; set; }

        public SessionCar Car { get; set; }

        public string Reason { get; set; }

        public string Penalty { get; set; }

        public int PenaltyValue { get; set; }

        public int ViolationInLap { get; set; }

        public int ClearedInLap { get; set; }
    }
}
