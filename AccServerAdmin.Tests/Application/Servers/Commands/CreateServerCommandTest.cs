using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Tests.Application.Servers.Commands
{
    [ExcludeFromCodeCoverage]
    public class CreateServerCommandTest
    {
        [Test]
        public async Task TestExecute()
        {
            // Arrange
            var id = Guid.NewGuid();
            var serverName = "Wibble server";
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var files = new List<string>
            {
                Path.Combine(settings.ServerBasePath, "File.1"),
                Path.Combine(settings.ServerBasePath, "File.2"),
                Path.Combine(settings.ServerBasePath, "File.3")
            };

            var server = new Server {Id = id, Name = serverName };
            var options = Substitute.For<IDataRepository<AppSettings>> ();
            var repo = Substitute.For<IDataRepository<Server>>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();

            options.GetAllAsync().Returns(new List<AppSettings> {settings});
            directory.GetFiles(settings.ServerBasePath).Returns(files);

            var command = new CreateServerCommand(options, repo, directory, file);

            // Act
            var returnServer = await command.ExecuteAsync(serverName);

            // Assert
            Assert.That(returnServer, Is.Not.Null);
            Assert.That(returnServer.Name, Is.EqualTo(serverName));
            Assert.That(returnServer.Id, Is.EqualTo(server.Id));
            
            directory.Received().GetFiles(settings.ServerBasePath);
            await repo.Received().AddAsync(server);

            file.Received().Copy(Path.Combine(settings.ServerBasePath, "File.1"), Path.Combine(settings.InstanceBasePath, server.Id.ToString(), "File.1"));
            file.Received().Copy(Path.Combine(settings.ServerBasePath, "File.2"), Path.Combine(settings.InstanceBasePath, server.Id.ToString(), "File.2"));
            file.Received().Copy(Path.Combine(settings.ServerBasePath, "File.3"), Path.Combine(settings.InstanceBasePath, server.Id.ToString(), "File.3"));
        }
    }
}
