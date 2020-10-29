using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccServerAdmin.Application
{
    public interface IProcessManager : IDisposable
    {
        IReadOnlyList<ServerProcessInfo> ServerProcesses { get; }

        double CpuUsage { get; }

        Task<ServerProcessInfo> StartServerAsync(Guid serverId);

        void StopServer(Guid serverId);
    }
}