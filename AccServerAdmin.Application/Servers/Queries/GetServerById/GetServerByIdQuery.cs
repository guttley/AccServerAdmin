using System;
using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;

namespace AccServerAdmin.Application.Servers.Queries.GetServerById
{
    public class GetServerByIdQuery : IGetServerByIdQuery
    {
        private readonly AppSettings _settings;

        public GetServerByIdQuery(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public Server Execute(Guid serverId)
        {
            throw new NotImplementedException();
        }
    }
}
