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
        private readonly PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
        private readonly PerformanceCounter _ramCounter = new PerformanceCounter("Process", "Working Set", true);

        public IReadOnlyList<ServerProcessInfo> ServerProcesses => _processes.Values.ToList();

        public double CpuUsage { get; private set; }
        public double RamUsage { get; private set; }

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
            var ticks = 0;

            while(!_disposed)
            {
                try
                {
                    ticks++;
                    Task.Delay(1000).Wait();

                    CpuUsage = _cpuCounter.NextValue();

                    if (ticks < 2)
                    {
                        continue;
                    }

                    ticks = 0;

                    var toCheck = new List<ServerProcessInfo>(_processes.Values);

                    foreach (var process in toCheck)
                    {
                        using (var processInfo = Process.GetProcessById(process.ProcessInfo.Id))
                        {
                            if (processInfo is null)
                            {
                                    _logger.LogInformation($"Server found not to be running: {process.ServerId}");
                                    _processes.TryRemove(process.ServerId, out _);
                                }
                        }
                    }

                } 
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error caught in process checker");
                }
            }

        }

        /*
        private double GetCpuUsageForProcess(Process process)
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = process.TotalProcessorTime;
            Task.Delay(500).Wait();
    
            var endTime = DateTime.UtcNow;
            var endCpuUsage = process.TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            return cpuUsageTotal * 100;
        }
        */

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

            var processInfo = new ServerProcessInfo(_services, serverId, server.Name, startInfo);
            processInfo.Start();

            _logger.LogInformation($"Server PID: {processInfo.ProcessInfo.Id}");

            _processes.AddOrUpdate(serverId, processInfo, (k, v) => processInfo);

            return processInfo;
        }

        public void StopServer(Guid serverId)
        {
            //var serverInstanceCleanUp = _scope.ServiceProvider.GetRequiredService<IServerInstanceCleanUp>();
            _processes.TryRemove(serverId, out var processInfo);

            _logger.LogInformation($"Stopping server PID: {processInfo.ProcessInfo.Id}");
            processInfo.Dispose();
            _logger.LogInformation("Server stopped");

            //await serverInstanceCleanUp.Execute(serverId);
        }

        public void Dispose()
        {
            foreach (var process in ServerProcesses)
            {
                process.Dispose();
            }

            _processCheckTask.Dispose();
            _disposed = true;
        }
    }
}
