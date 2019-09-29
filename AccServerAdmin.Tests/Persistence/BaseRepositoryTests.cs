using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Common;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Persistence
{
    [ExcludeFromCodeCoverage]
    public class BaseRepositoryTests
    {
        [Test]
        public void Test_New()
        {
            // Arrange
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var repo = new BaseConfigRepository<object>(directory, file, converter, "cfg", "test.json");

            // Act / Assert
            Assert.Throws<NotImplementedException>(() => repo.New());
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
            var testPath = Path.Combine(serverPath, "cfg", "test.json");
            var obj = new object();

            options.Read().Returns(settings);
            file.Exists(testPath).Returns(true);
            converter.DeserializeObject<object>(Arg.Any<string>()).Returns(obj);

            var repo = new BaseConfigRepository<object>(directory, file, converter, "cfg", "test.json");

            // Act 
            var returned = repo.Read(serverPath);

            // Assert
            Assert.That(returned, Is.EqualTo(obj));
            file.Received().Exists(testPath);
            file.Received().ReadAllText(Path.Combine(serverPath, "cfg", "test.json"));
            converter.Received().DeserializeObject<object>(Arg.Any<string>());
        }

        [Test]
        public void Test_ReadThrowsNotImplementedException()
        {
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var options = Substitute.For<IAppSettingsRepository> ();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var testPath = Path.Combine(serverPath, "cfg", "test.json");
            var obj = new object();
            
            options.Read().Returns(settings);
            file.Exists(testPath).Returns(false);
            converter.DeserializeObject<object>(Arg.Any<string>()).Returns(obj);

            var repo = new BaseConfigRepository<object>(directory, file, converter, "cfg", "test.json");

            // Act / Assert
            Assert.Throws<NotImplementedException>(() => repo.Read(serverPath));
        }

        [Test]
        public void Test_Save()
        {
            var serverId = Guid.NewGuid();
            var settings = new AppSettings { ServerBasePath = "C:\\FakeBasePath", InstanceBasePath = "C:\\FakeInstancePath" };
            var serverPath = Path.Combine(settings.InstanceBasePath, serverId.ToString());
            var options = Substitute.For<IAppSettingsRepository> ();
            var directory = Substitute.For<IDirectory>();
            var file = Substitute.For<IFile>();
            var converter = Substitute.For<IJsonConverter>();
            var testPath = Path.Combine(serverPath, "AccAdmin.json");
            var obj = new object();

            options.Read().Returns(settings);
            file.Exists(testPath).Returns(true);
            converter.DeserializeObject<object>(Arg.Any<string>()).Returns(obj);
            directory.Exists(Arg.Any<string>()).Returns(false);

            var repo = new BaseConfigRepository<object>(directory, file, converter, "cfg", "test.json");

            // Act 
            repo.Save(serverPath, obj);

            // Assert
            directory.Received().Exists(Arg.Any<string>());
            directory.CreateDirectory(Arg.Any<string>());
            converter.Received().SerializeObject(Arg.Any<object>());
            file.WriteAllText(Path.Combine(serverPath, "cfg", "test.json"), Arg.Any<string>());
        }
    }
}
