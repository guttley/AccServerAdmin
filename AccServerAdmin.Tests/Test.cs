using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Persistence.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests
{
    
    public class Test
    {
        [Test]
        public async Task ReadResult()
        {
            var serverId = Guid.Parse("5e2eecf4-330a-4ad1-b8fc-a2edd49bf7be");
            var driverRepo = Substitute.For<IDriverRepository>();
            var logger = Substitute.For<ILogger<ResultImporter>>();
            var serverPathResolver = Substitute.For<IServerPathResolver>();
            var jsonConverter = new JsonDotNetConverter();
            var file = new FileApiWrapper();
            var importer = new ResultImporter(logger, driverRepo, serverPathResolver, jsonConverter, file);

            serverPathResolver.Execute(serverId).Returns("C:\\ProgramData\\AccServerAdmin\\ServerInstances\\5e2eecf4-330a-4ad1-b8fc-a2edd49bf7be");

            await importer.Execute(serverId);

        }
    }
}
