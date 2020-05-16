using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.Results;
using AccServerAdmin.Infrastructure.Helpers;
using AccServerAdmin.Infrastructure.IO;
using AccServerAdmin.Notifications.Results;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests
{
    
    public class Test
    {
        /*
        [Test]
        public async Task ReadResult()
        {
            var serverId = Guid.Parse("5e2eecf4-330a-4ad1-b8fc-a2edd49bf7be");
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var sessionRepo = Substitute.For<IDataRepository<Session>>();
            var sessionDriverRepo = Substitute.For<IDataRepository<SessionCar>>();
            var driverRepo = Substitute.For<IDriverRepository>();
            var hubContext = Substitute.For<IHubContext<ResultImportHub, IResultImport>>();
            var serverPathResolver = Substitute.For<IServerPathResolver>();
            var jsonConverter = new JsonDotNetConverter();
            var file = new FileApiWrapper();
            var importer = new ResultImporter(hubContext, driverRepo, sessionRepo, sessionDriverRepo, unitOfWork, serverPathResolver, jsonConverter, file);

            serverPathResolver.Execute(serverId).Returns("C:\\ProgramData\\AccServerAdmin\\ServerInstances\\5e2eecf4-330a-4ad1-b8fc-a2edd49bf7be");

            await importer.Execute(serverId, "Wibble");

        }
        */
    }
}
