using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain
{
    public class SessionDriver : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Driver Driver { get; set; }

        public CarModel CarModel { get; set; }

        public long CupCategory { get; set; }

        public long RaceNumber { get; set; }

        public string TeamName { get; set; }

    }
}
