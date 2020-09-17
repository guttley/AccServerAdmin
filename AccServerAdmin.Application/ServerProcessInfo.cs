using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results;
using Microsoft.Extensions.DependencyInjection;

namespace AccServerAdmin.Application
{
    /// <summary>
    /// Encapsulates the process info for a running server instance
    /// </summary>
    public class ServerProcessInfo : IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly string _serverName;
        private readonly FileSystemWatcher _watcher;
        
        public Guid ServerId { get; }

        public Process ProcessInfo { get; private set; }

        public ProcessStartInfo StartInfo { get; }


        public ServerProcessInfo(IServiceProvider services, Guid serverId, string serverName, ProcessStartInfo startInfo)
        {
            _services = services;
            _serverName = serverName;
            ServerId = serverId;
            StartInfo = startInfo;

            _watcher = new FileSystemWatcher($"{startInfo.WorkingDirectory}\\results\\");
            _watcher.Created += LogCreated;
            _watcher.EnableRaisingEvents = true;
        }

        private void LogCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
                return;

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(10000);
                    await ImportFile(e);
                }
                catch (Exception ex)
                {

                }
            });
        }

        private async Task ImportFile(FileSystemEventArgs fileSystemEventArgs)
        {
            using var scope = _services.CreateScope();
            var import = scope.ServiceProvider.GetRequiredService<IResultImporter>();

            await import.Execute(ServerId, _serverName);
        }

        public void Start()
        {
            ProcessInfo = Process.Start(StartInfo);
        }

        public void Dispose()
        {
            ProcessInfo.Kill(true);
            ProcessInfo?.Dispose();
            _watcher.Dispose();
        }
    }
}