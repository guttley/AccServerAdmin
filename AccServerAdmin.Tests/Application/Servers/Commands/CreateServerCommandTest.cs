using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Domain;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

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
            var repo = Substitute.For<IServerRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            
            options.GetAll().Returns(new List<AppSettings> {settings});

            var command = new CreateServerCommand(repo, unitOfWork);

            // Act
            var returnServer = await command.Execute(serverName);

            // Assert
            Assert.That(returnServer, Is.Not.Null);
            Assert.That(returnServer.Name, Is.EqualTo(serverName));
            Assert.That(returnServer.Id, Is.EqualTo(server.Id));

            await unitOfWork.Received().SaveChanges();
            await repo.Received().Add(server);
        }
    }
}
