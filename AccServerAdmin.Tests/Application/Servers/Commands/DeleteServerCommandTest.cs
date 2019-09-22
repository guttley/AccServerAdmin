using AccServerAdmin.Infrastructure.IO;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Application.Common;
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
            var instanceBasePath = "C:\\FakeInstancePath";
            var dirs = new List<string>
            {
                Path.Combine(instanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(instanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(instanceBasePath , Guid.NewGuid().ToString()),
                Path.Combine(instanceBasePath, serverId.ToString())
            };

            var serverResolver = Substitute.For<IServerDirectoryResolver>();
            var directory = Substitute.For<IDirectory>();

            serverResolver.Resolve(serverId).Returns(Path.Combine(instanceBasePath, serverId.ToString()));
            directory.GetDirectories(instanceBasePath).Returns(dirs);

            var command = new DeleteServerCommand(serverResolver, directory);

            // Act
            command.Execute(serverId);

            // Assert
           
            serverResolver.Received().Resolve(serverId);
            directory.Received(1).Delete(Path.Combine(instanceBasePath, serverId.ToString()), true);

        }
    }
}
