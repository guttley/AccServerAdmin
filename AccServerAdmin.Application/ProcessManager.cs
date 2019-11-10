﻿using AccServerAdmin.Domain;
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
        private readonly Task _processCheckTask;

        public IReadOnlyList<ServerProcessInfo> ServerProcesses => _processes.Values.ToList();

        public ProcessManager(
            IServiceProvider services,
            ILogger<ProcessManager> logger)
        {
            _scope = services.CreateScope();
            _logger = logger;
            _processCheckTask = Task.Run(CheckProcesses);
        }
        
        private void CheckProcesses()
        {
            while(_scope != null)
            {
                try
                {
                    var toCheck = new List<ServerProcessInfo>(_processes.Values);

                    foreach (var process in toCheck)
                    {
                        if (Process.GetProcessById(process.ProcessInfo.Id) == null)
                        {
                            _logger.LogInformation($"Server found not to be running: {process.ServerId}");
                            _processes.TryRemove(process.ServerId, out _);
                        }
                    }

                    Task.Delay(1000);
                } 
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error caught in process checker");
                }
            }

        }

        public async Task<ServerProcessInfo> StartServerAsync(Guid serverId)
        {
            var getServerByIdQuery = _scope.ServiceProvider.GetRequiredService<IGetServerByIdQuery>();
            var serverConfigWriter = _scope.ServiceProvider.GetRequiredService<IServerConfigWriter>();
            var serverInstanceCreator = _scope.ServiceProvider.GetRequiredService<IServerInstanceCreator>();
            var server = await getServerByIdQuery.ExecuteAsync(serverId).ConfigureAwait(false);

            var path = await serverInstanceCreator.ExecuteAsync(server).ConfigureAwait(false);

            serverConfigWriter.Execute(server, path);

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

            //await serverInstanceCleanUp.ExecuteAsync(serverId).ConfigureAwait(false);
        }

        public void Dispose()
        {
            foreach (var process in ServerProcesses)
            {
                process.ProcessInfo.Kill(true);
            }

            _scope?.Dispose();
            _scope = null;
        }
    }
}
