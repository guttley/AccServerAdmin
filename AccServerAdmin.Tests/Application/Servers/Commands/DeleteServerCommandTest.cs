using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.Servers.Commands;

namespace AccServerAdmin.Tests.Application.Servers.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteServerCommandTest
    {
        [Test]
        public void TestExecute()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, serverId.ToString())
            };

            var options = Substitute.For<IOptions<AppSettings>>();
            var directory = Substitute.For<IDirectory>();

            options.Value.Returns(settings);
            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);

            var command = new DeleteServerCommand(options, directory);

            // Act
            command.Execute(serverId);

            // Assert
           
            directory.Received().GetDirectories(settings.InstanceBasePath);
            directory.Received(1).Delete(Path.Combine(settings.InstanceBasePath, serverId.ToString()), true);

        }

        [Test]
        public void TestExecute_ThrowsKeyNotFound()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { InstanceBasePath = "C:\\FakeInstancePath" };
            var dirs = new List<string>
            {
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
                Path.Combine(settings.InstanceBasePath, Guid.NewGuid().ToString()),
            };

            var options = Substitute.For<IOptions<AppSettings>>();
            var directory = Substitute.For<IDirectory>();

            options.Value.Returns(settings);
            directory.GetDirectories(settings.InstanceBasePath).Returns(dirs);

            var command = new DeleteServerCommand(options, directory);

            // Act / Assert
            Assert.Throws<KeyNotFoundException>(() => command.Execute(serverId));
        }
    }
}
