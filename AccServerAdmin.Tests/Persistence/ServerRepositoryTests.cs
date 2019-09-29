using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Server;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class ServerRepositoryTests
    {
        [Test]
        public void Test_New()
        {
            // Arrange
            var serverName = "Wibble Server";
            var settings = new AppSettings
                {ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath"};
            var options = Substitute.For<IAppSettingsRepository>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();

            options.Read().Returns(settings);

            var repo = new ServerRepository(options, directory, file, converter);

            // Act 
            var server = repo.New(serverName);

            // Assert
            Assert.That(server, Is.Not.Null);
            Assert.That(server.Name, Is.EqualTo(serverName));
            Assert.That(server.Location, Is.EqualTo(Path.Combine(settings.InstanceBasePath, server.Id.ToString())));
        }

        [Test]
        public void Test_Read()
        {
            var serverId = Guid.NewGuid();
            var settings = new AppSettings {ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath"};
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var options = Substitute.For<IAppSettingsRepository>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var testPath = Path.Combine(serverPath, "AccAdmin.json");
            var server = new Server
            {
                Id = serverId,
                Name = "Wibble",
                Location = testPath
            };

            options.Read().Returns(settings);
            file.Exists(testPath).Returns(true);
            converter.DeserializeObject<Server>(Arg.Any<string>()).Returns(server);

            var repo = new ServerRepository(options, directory, file, converter);

            // Act 
            var returnServer = repo.Read(serverPath);

            // Assert
            Assert.That(returnServer, Is.EqualTo(server));
            file.Received().Exists(testPath);
            file.Received().ReadAllText(Path.Combine(serverPath, "AccAdmin.json"));
            converter.Received().DeserializeObject<Server>(Arg.Any<string>());
        }

        [Test]
        public void Test_ReadThrowsArgumentException()
        {
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var options = Substitute.For<IAppSettingsRepository>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var testPath = Path.Combine(serverPath, "AccAdmin.json");
            var server = new Server
            {
                Id = serverId,
                Name = "Wibble",
                Location = testPath
            };

            options.Read().Returns(settings);
            file.Exists(testPath).Returns(false);
            converter.DeserializeObject<Server>(Arg.Any<string>()).Returns(server);

            var repo = new ServerRepository(options, directory, file, converter);

            // Act / Assert
            Assert.Throws<ArgumentException>(() => repo.Read(serverPath));
        }

        [Test]
        public void Test_Save()
        {
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var options = Substitute.For<IAppSettingsRepository>();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var testPath = Path.Combine(serverPath, "AccAdmin.json");
            var server = new Server
            {
                Id = serverId,
                Name = "Wibble",
                Location = testPath
            };

            options.Read().Returns(settings);
            file.Exists(testPath).Returns(true);
            converter.DeserializeObject<Server>(Arg.Any<string>()).Returns(server);
            directory.Exists(Arg.Any<string>()).Returns(false);

            var repo = new ServerRepository(options, directory, file, converter);

            // Act 
            repo.Save(server);

            // Assert
            directory.Received().Exists(Path.GetDirectoryName(server.Location));
            directory.CreateDirectory(Path.GetDirectoryName(server.Location));
            converter.Received().SerializeObject(Arg.Any<Server>());
            file.WriteAllText(Path.Combine(serverPath, "AccAdmin.json"), Arg.Any<string>());
        }
    }
}
