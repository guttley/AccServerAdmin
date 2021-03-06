﻿using AccServerAdmin.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Servers.Queries
{
    public class GetServerListQuery : IGetServerListQuery
    {
        private readonly IServerRepository _serverRepository;

        public GetServerListQuery(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<IEnumerable<Server>> Execute()
        {
            return await _serverRepository.GetAll();
        }
    }
}
