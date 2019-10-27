using AccServerAdmin.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Servers.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AccServerAdmin.Application
{
    public class ProcessManager : IProcessManager
    {
        private readonly IServiceScope _scope;
        private readonly ILogger<ProcessManager> _logger;
        private readonly ConcurrentDictionary<Guid, ServerProcessInfo> _processes = new ConcurrentDictionary<Guid, ServerProcessInfo>();

        public IReadOnlyList<ServerProcessInfo> ServerProcesses => _processes.Values.ToList();

        public ProcessManager(
            IServiceProvider services,
            ILogger<ProcessManager> logger)
        {
            _scope = services.CreateScope();
            _logger = logger;
        }
        
        public async Task<ServerProcessInfo> StartServerAsync(Guid serverId)
        {
            var getServerByIdQuery = _scope.ServiceProvider.GetRequiredService<IGetServerByIdQuery>();
            var serverDirectoryResolver = _scope.ServiceProvider.GetRequiredService<IServerDirectoryResolver>();
            var serverConfigWriter = _scope.ServiceProvider.GetRequiredService<IServerConfigWriter>();

            var server = await getServerByIdQuery.ExecuteAsync(serverId).ConfigureAwait(false);
            var path = await serverDirectoryResolver.ResolveAsync(serverId).ConfigureAwait(false);

            await serverConfigWriter.ExecuteAsync(serverId).ConfigureAwait(false);

            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = path,
                WindowStyle = ProcessWindowStyle.Hidden,
                //Arguments
            };

            _logger.LogInformation($"Starting server: {server.Name}");

            var processInfo = new ServerProcessInfo
            {
                ProcessInfo = Process.Start(startInfo),
                ServerId = serverId,
            };

            _logger.LogInformation($"Server PID: {processInfo.ProcessInfo.Id}");

            _processes.AddOrUpdate(serverId, processInfo, (k, v) => processInfo);
            return processInfo;
        }


        public void StopServer(Guid serverId)
        {
            _processes.TryRemove(serverId, out var processInfo);

            _logger.LogInformation($"Stopping server PID: {processInfo.ProcessInfo.Id}");
            processInfo.ProcessInfo.Kill(true);
            _logger.LogInformation("Server stopped");
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
