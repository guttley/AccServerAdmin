using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Service.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ServerControllerTests
    {
        private Server _server1;
        private Server _server2;
        private Server _server3;
        private Server _server4;
        private Server _server5;
        private List<Server> _servers;

        [SetUp]
        public void Setup()
        {
            _server1 = new Server {Id = Guid.Parse("34273142-1A7A-470C-BAC2-000000000001"), Name = "Server 1"};
            _server2 = new Server {Id = Guid.Parse("34273142-1A7A-470C-BAC2-000000000002"), Name = "Server 2"};
            _server3 = new Server {Id = Guid.Parse("34273142-1A7A-470C-BAC2-000000000003"), Name = "Server 3"};
            _server4 = new Server {Id = Guid.Parse("34273142-1A7A-470C-BAC2-000000000004"), Name = "Server 4"};
            _server5 = new Server {Id = Guid.Parse("34273142-1A7A-470C-BAC2-000000000005"), Name = "Server 5"}; 
            
            _servers = new List<Server>
            {
                _server1,
                _server2,
                _server3,
                _server4,
                _server5
            };
        }

        [Test]
        public void GetsListOfServers()
        {
            // Arrange
            var createCommand = Substitute.For<ICreateServerCommand>();
            var updateCommand = Substitute.For<IUpdateServerCommand>();
            var deleteCommand = Substitute.For<IDeleteServerCommand>();
            var getByIdCommand = Substitute.For<IGetServerByIdQuery>();
            var getListCommand = Substitute.For<IGetServerListQuery>();
            var controller = new ServerController(createCommand, deleteCommand, updateCommand, getByIdCommand, getListCommand);

            getListCommand.Execute().Returns(_servers);

            // Act
            var servers = controller.GetServerList();

            // Assert
            getListCommand.Received().Execute();
            Assert.That(servers, Is.EquivalentTo(_servers));
        }

        [TestCase("34273142-1A7A-470C-BAC2-000000000001", "Server 1")]
        [TestCase("34273142-1A7A-470C-BAC2-000000000002", "Server 2")]
        [TestCase("34273142-1A7A-470C-BAC2-000000000003", "Server 3")]
        [TestCase("34273142-1A7A-470C-BAC2-000000000004", "Server 4")]
        [TestCase("34273142-1A7A-470C-BAC2-000000000005", "Server 5")]
        public void GetsServerById(string serverId, string serverName)
        {
            // Arrange
            var searchGuid = Guid.Parse(serverId);
            var createCommand = Substitute.For<ICreateServerCommand>();
            var updateCommand = Substitute.For<IUpdateServerCommand>();
            var deleteCommand = Substitute.For<IDeleteServerCommand>();
            var getByIdCommand = Substitute.For<IGetServerByIdQuery>();
            var getListCommand = Substitute.For<IGetServerListQuery>();
            var controller = new ServerController(createCommand, deleteCommand, updateCommand, getByIdCommand, getListCommand);

            getByIdCommand.Execute(Arg.Is(_server1.Id)).Returns(_server1);
            getByIdCommand.Execute(Arg.Is(_server2.Id)).Returns(_server2);
            getByIdCommand.Execute(Arg.Is(_server3.Id)).Returns(_server3);
            getByIdCommand.Execute(Arg.Is(_server4.Id)).Returns(_server4);
            getByIdCommand.Execute(Arg.Is(_server5.Id)).Returns(_server5);

            // Act
            var server = controller.GetServerById(searchGuid);

            // Assert
            getByIdCommand.Received().Execute(searchGuid);
            Assert.That(server.Id, Is.EqualTo(searchGuid));
            Assert.That(server.Name, Is.EqualTo(serverName));
        }

        [Test]
        public void CreatesServer()
        {
            // Arrange
            var createCommand = Substitute.For<ICreateServerCommand>();
            var updateCommand = Substitute.For<IUpdateServerCommand>();
            var deleteCommand = Substitute.For<IDeleteServerCommand>();
            var getByIdCommand = Substitute.For<IGetServerByIdQuery>();
            var getListCommand = Substitute.For<IGetServerListQuery>();
            var controller = new ServerController(createCommand, deleteCommand, updateCommand, getByIdCommand, getListCommand);

            createCommand.Execute(Arg.Is(_server1.Name)).Returns(_server1);

            // Act
            var server = controller.CreateServer(_server1.Name);

            // Assert
            createCommand.Received().Execute(_server1.Name);
            Assert.That(server.Id, Is.EqualTo(_server1.Id));
            Assert.That(server.Name, Is.EqualTo(_server1.Name));
        }

        [Test]
        public void UpdatesServer()
        {
            // Arrange
            var newServerName = "New server name - wibble";
            var createCommand = Substitute.For<ICreateServerCommand>();
            var updateCommand = Substitute.For<IUpdateServerCommand>();
            var deleteCommand = Substitute.For<IDeleteServerCommand>();
            var getByIdCommand = Substitute.For<IGetServerByIdQuery>();
            var getListCommand = Substitute.For<IGetServerListQuery>();
            var controller = new ServerController(createCommand, deleteCommand, updateCommand, getByIdCommand, getListCommand);

            // Act
            controller.UpdateServer(_server1.Id, newServerName);

            // Assert
            updateCommand.Received().Execute(_server1.Id, newServerName);
        }

        [Test]
        public void DeletesServer()
        {
            // Arrange
            var createCommand = Substitute.For<ICreateServerCommand>();
            var updateCommand = Substitute.For<IUpdateServerCommand>();
            var deleteCommand = Substitute.For<IDeleteServerCommand>();
            var getByIdCommand = Substitute.For<IGetServerByIdQuery>();
            var getListCommand = Substitute.For<IGetServerListQuery>();
            var controller = new ServerController(createCommand, deleteCommand, updateCommand, getByIdCommand, getListCommand);

            // Act
            controller.DeleteServer(_server1.Id);

            // Assert
            deleteCommand.Received().Execute(_server1.Id);
        }

    }
}