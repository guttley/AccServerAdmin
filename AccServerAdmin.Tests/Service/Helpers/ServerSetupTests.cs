using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ServerSetupTests
    {
        [Test]
        public void SetupCopiesFiles()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings {InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            options.Value.Returns(settings);

            var server = new Server
            {
                Id = Guid.Parse("A94833AB-37DA-4153-8A57-860F0BFFD39F"),
                Name = "Test Server", 
                Location = "C:\\MyInstances\\A94833AB-37DA-4153-8A57-860F0BFFD39F"
            };

            var files = new List<string>
            {
                Path.Combine(settings.ServerBasePath, "file.1"), 
                Path.Combine(settings.ServerBasePath, "file.2"), 
                Path.Combine(settings.ServerBasePath, "file.3"), 
            };

            directory.GetFiles(settings.ServerBasePath).Returns(files);

            // Act
            serverSetup.Execute(server);

            // Assert
            directory.Received().GetFiles(settings.ServerBasePath);
            file.Received().Copy(files[0],  Path.Combine(server.Location, "file.1"));
            file.Received().Copy(files[1],  Path.Combine(server.Location, "file.2"));
            file.Received().Copy(files[2],  Path.Combine(server.Location, "file.3"));
        }
    }
}
