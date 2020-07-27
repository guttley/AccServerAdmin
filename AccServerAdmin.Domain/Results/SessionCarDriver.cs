using System;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain.Results
{
    public class SessionCarDriver : IKeyedEntity
    {
        [NotMapped]
        public Guid Id
        {
            get => SessionCarId;
            set => SessionCarId = value;
        }

        public Guid SessionCarId { get; set; }

        public SessionCar Car { get; set; }

        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}
