using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccServerAdmin.Domain
{
    public class SessionLap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public SessionDriver Driver { get; set; }

        public long Laptime { get; set; }

        public long Split1 { get; set; }

        public long Split2 { get; set; }

        public long Split3 { get; set; }

    }
}
