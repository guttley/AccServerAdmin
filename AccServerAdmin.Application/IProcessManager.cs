using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application
{
    public interface IProcessManager : IDisposable
    {
        IReadOnlyList<ServerProcessInfo> ServerProcesses { get; }
        Task<ServerProcessInfo> StartServerAsync(Guid serverId);
        void StopServer(Guid serverId);
    }
}