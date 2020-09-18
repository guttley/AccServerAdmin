using System;

namespace AccServerAdmin.Domain.AccConfig
{
    public class DriverEntry
    {
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }

        public Guid EntryId { get; set; }

        public Entry Entry { get; set; }

        public int DriverNumber { get; set; }
    }
}
