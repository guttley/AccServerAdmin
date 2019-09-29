using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Application.Servers.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetServerListTests
    { 
        /*
        [Test]
        public void TestExecute()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var serverName = "TestFoo Server";
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var server = new Server {Id = serverId, Name = serverName};
            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                serverPath
            };

            var options = Substitute.For<IDataRepository<AppSettings>> ();
            var repo = Substitute.For<IServerRepository>();
            var directory = Substitute.For<IDirectory>();

            options.GetAll().Returns(new List<AppSettings> {settings});
            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);
            repo.Read(serverPath).Returns(server);

            var command = new GetServerListQuery(options, repo, directory);

            // Act
            var returnedServers = command.Execute();

            // Assert
            directory.Received().GetDirectories(settings.InstanceBasePath);

            foreach (var dir in dirs)
            {
                repo.Received().Read(dir);
            }
        }
        */
    }
}
