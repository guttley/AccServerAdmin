﻿using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.ServerConfig;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class ServerConfigRepositoryTests
    {
        [Test]
        public void Test_New()
        {
            // Arrange
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();

            var repo = new ServerConfigRepository(directory, file, converter);

            // Act 
            var config = repo.New();

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.MaxClients, Is.EqualTo(ServerConfigRepository.DefaultMaxClients));
            Assert.That(config.RegisterToLobby, Is.EqualTo(ServerConfigRepository.DefaultRegisterToLobby));
            Assert.That(config.TcpPort, Is.EqualTo(ServerConfigRepository.DefaultTcpPort));
            Assert.That(config.UdpPort, Is.EqualTo(ServerConfigRepository.DefaultUdpPort));
            Assert.That(config.Version, Is.EqualTo(ServerConfigRepository.DefaultConfigVersion));
        }

        [Test]
        public void Test_ReadReturnsNewIfDoesNotExist()
        {
            // Arrange
            var path = "C:\\FakeInstancePath";
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var repo = new ServerConfigRepository(directory, file, converter);

            file.Exists(Arg.Any<string>()).Returns(false);

            // Act 
            var config = repo.Read(path);

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.MaxClients, Is.EqualTo(ServerConfigRepository.DefaultMaxClients));
            Assert.That(config.RegisterToLobby, Is.EqualTo(ServerConfigRepository.DefaultRegisterToLobby));
            Assert.That(config.TcpPort, Is.EqualTo(ServerConfigRepository.DefaultTcpPort));
            Assert.That(config.UdpPort, Is.EqualTo(ServerConfigRepository.DefaultUdpPort));
            Assert.That(config.Version, Is.EqualTo(ServerConfigRepository.DefaultConfigVersion));
        }
    }
}