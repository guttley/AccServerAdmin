using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain.Results
{
    public class SessionCar : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SessionId { get; set; }

        public List<SessionCarDriver> Drivers { get; set; } = new List<SessionCarDriver>();

        public CarModel CarModel { get; set; }

        public long CupCategory { get; set; }

        public long RaceNumber { get; set; }

        public string TeamName { get; set; }

    }
}
