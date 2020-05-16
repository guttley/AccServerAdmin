using System;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain.Results
{
    public class SessionCarDriver
    {
        public Guid SessionCarId { get; set; }

        public SessionCar Car { get; set; }

        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}
