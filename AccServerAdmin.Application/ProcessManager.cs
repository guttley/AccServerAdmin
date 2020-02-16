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
        private volatile bool _disposed;
        private readonly IServiceProvider _services;
        private readonly ILogger<ProcessManager> _logger;
        private readonly ConcurrentDictionary<Guid, ServerProcessInfo> _processes = new ConcurrentDictionary<Guid, ServerProcessInfo>();
        private readonly Task _processCheckTask;

        public IReadOnlyList<ServerProcessInfo> ServerProcesses => _processes.Values.ToList();

        public ProcessManager(
            IServiceProvider services,
            ILogger<ProcessManager> logger)
        {
            _services = services;            
            _logger = logger;
            _processCheckTask = Task.Run(CheckProcesses);
        }
        
        private void CheckProcesses()
        {
            while(!_disposed)
            {
                try
                {
                    var toCheck = new List<ServerProcessInfo>(_processes.Values);

                    foreach (var process in toCheck)
                    {
                        using var processInfo = Process.GetProcessById(process.ProcessInfo.Id);
                        if (processInfo == null)
                        {
                            _logger.LogInformation($"Server found not to be running: {process.ServerId}");
                            _processes.TryRemove(process.ServerId, out _);
                        }
                    }

                    Task.Delay(3000).Wait();
                } 
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error caught in process checker");
                }
            }

        }

        public async Task<ServerProcessInfo> StartServerAsync(Guid serverId)
        {
            using var scope = _services.CreateScope();
            var getServerByIdQuery = scope.ServiceProvider.GetRequiredService<IGetServerByIdQuery>();
            var serverConfigWriter = scope.ServiceProvider.GetRequiredService<IServerConfigWriter>();
            var serverInstanceCreator = scope.ServiceProvider.GetRequiredService<IServerInstanceCreator>();
            var server = await getServerByIdQuery.Execute(serverId);
            var path = await serverInstanceCreator.Execute(server);

            await serverConfigWriter.Execute(server, path);

            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = "accServer.exe",
                UseShellExecute = true,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = path
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
            //var serverInstanceCleanUp = _scope.ServiceProvider.GetRequiredService<IServerInstanceCleanUp>();

            _processes.TryRemove(serverId, out var processInfo);

            _logger.LogInformation($"Stopping server PID: {processInfo.ProcessInfo.Id}");
            processInfo.ProcessInfo.Kill(true);
            _logger.LogInformation("Server stopped");

            //await serverInstanceCleanUp.Execute(serverId);
        }

        public void Dispose()
        {
            foreach (var process in ServerProcesses)
            {
                process.ProcessInfo.Kill(true);
            }

            _disposed = true;
        }
    }
}
