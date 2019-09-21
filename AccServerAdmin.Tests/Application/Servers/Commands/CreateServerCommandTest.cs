using AccServerAdmin.Application.Servers.Commands.CreateServer;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AccServerAdmin.Tests.Application.Servers.Commands
{
    [ExcludeFromCodeCoverage]
    public class CreateServerCommandTest
    {
        [Test]
        public void TestExecute()
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

            var server = new Server {Id = id, Name = serverName, Location = Path.Combine(settings.InstanceBasePath, id.ToString()) };
            var options = Substitute.For<IOptions<AppSettings>>();
            var repo = Substitute.For<IServerRepository>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();

            options.Value.Returns(settings);
            repo.New(serverName).Returns(server);
            directory.GetFiles(settings.ServerBasePath).Returns(files);

            var command = new CreateServerCommand(options, repo, directory, file);

            // Act
            var returnServer = command.Execute(serverName);

            // Assert
            Assert.That(returnServer, Is.Not.Null);
            Assert.That(returnServer.Name, Is.EqualTo(serverName));
            Assert.That(returnServer.Id, Is.EqualTo(server.Id));
            
            directory.Received().GetFiles(settings.ServerBasePath);
            repo.Received().Save(server);

            file.Received().Copy($"{settings.ServerBasePath}\\File.1", $"{settings.InstanceBasePath}\\{server.Id}\\File.1");
            file.Received().Copy($"{settings.ServerBasePath}\\File.2", $"{settings.InstanceBasePath}\\{server.Id}\\File.2");
            file.Received().Copy($"{settings.ServerBasePath}\\File.3", $"{settings.InstanceBasePath}\\{server.Id}\\File.3");
        }
    }
}
