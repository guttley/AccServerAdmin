using System;
using System.Diagnostics;

namespace AccServerAdmin.Domain
{
    /// <summary>
    /// Encapsulates the process info for a running server instance
    /// </summary>
    public class ServerProcessInfo
    {
        public Guid ServerId { get; set; }

        public Process ProcessInfo { get; set; }
    }
}
