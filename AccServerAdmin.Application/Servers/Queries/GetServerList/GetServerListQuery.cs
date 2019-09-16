using AccServerAdmin.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AccServerAdmin.Application.Servers.Queries.GetServerList
{
    public class GetServerListQuery : IGetServerListQuery
    {
        private readonly AppSettings _settings;

        public GetServerListQuery(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public IEnumerable<Server> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
