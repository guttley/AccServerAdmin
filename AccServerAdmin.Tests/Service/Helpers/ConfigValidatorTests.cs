using System.Diagnostics.CodeAnalysis;
using System.IO;
using AccServerAdmin.Domain;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests.Service.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ConfigValidatorTests
    {
        [Test]
        public void CreatesBaseInstancePath()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings { InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            options.Value.Returns(settings);
            var validator = new ConfigValidator(directory, file, options);
            file.Exists(Arg.Any<string>()).Returns(true);
            directory.Exists(Arg.Any<string>()).Returns(false);
            directory.Exists(Arg.Is(settings.ServerBasePath)).Returns(true);

            // Act
            validator.Execute();

            // Assert
            directory.Received().Exists(settings.InstanceBasePath);
            directory.Received().CreateDirectory(settings.InstanceBasePath);
        }

        [Test]
        public void DoesNotCreateBaseInstancePath()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings { InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            options.Value.Returns(settings);
            var validator = new ConfigValidator(directory, file, options);
            file.Exists(Arg.Any<string>()).Returns(true);
            directory.Exists(Arg.Any<string>()).Returns(true);
            directory.Exists(Arg.Is(settings.ServerBasePath)).Returns(true);

            // Act
            validator.Execute();

            // Assert
            directory.Received().Exists(settings.InstanceBasePath);
            directory.DidNotReceive().CreateDirectory(settings.InstanceBasePath);
        }

        [Test]
        public void ServerBasePathNotExist_Throws()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings { InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            options.Value.Returns(settings);
            var validator = new ConfigValidator(directory, file, options);
            file.Exists(Arg.Any<string>()).Returns(true);
            directory.Exists(Arg.Any<string>()).Returns(true);
            directory.Exists(Arg.Is(settings.ServerBasePath)).Returns(false);

            // Act
            var ex = Assert.Throws<DirectoryNotFoundException>(() => validator.Execute());

            // Assert
            directory.Received().Exists(settings.ServerBasePath);
        }

        [Test]
        public void ServerBaseExeNotExist_Throws()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings { InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            var path = Path.Combine(settings.ServerBasePath, "accServer.exe");
            options.Value.Returns(settings);
            var validator = new ConfigValidator(directory, file, options);
            file.Exists(Arg.Any<string>()).Returns(true);
            file.Exists(path).Returns(false);
            directory.Exists(Arg.Any<string>()).Returns(true);

            // Act / Assert
            var ex = Assert.Throws<FileNotFoundException>(() => validator.Execute());
        }

        [Test]
        public void ServerBasePdbNotExist_Throws()
        {
            // Arrange
            var file = Substitute.For<IFile>();
            var directory = Substitute.For<IDirectory>();
            var settings = new AppSettings { InstanceBasePath = "C:\\MyInstances", ServerBasePath = "C:\\ServerBase"};
            var options = Substitute.For<IOptions<AppSettings>>();
            var path = Path.Combine(settings.ServerBasePath, "accServer.pdb");
            options.Value.Returns(settings);
            var validator = new ConfigValidator(directory, file, options);
            file.Exists(Arg.Any<string>()).Returns(true);
            file.Exists(path).Returns(false);
            directory.Exists(Arg.Any<string>()).Returns(true);

            // Act / Assert
            Assert.Throws<FileNotFoundException>(() => validator.Execute());
        }
    }
}
