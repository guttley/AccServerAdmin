﻿using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.ServerConfig;

namespace AccServerAdmin.Application.ServerConfig.Queries
{
    public class GetServerConfigByIdQuery : IGetServerConfigByIdQuery
    {
        private readonly IServerDirectoryResolver _serverResolver;
        private readonly IServerConfigRepository _configRepository;

        public GetServerConfigByIdQuery(
            IServerDirectoryResolver serverResolver,
            IServerConfigRepository configRepository)
        {
            _serverResolver = serverResolver;
            _configRepository = configRepository;
        }

        public Configuration Execute(Guid serverId)
        {
            var path = _serverResolver.Resolve(serverId);
            var config = _configRepository.Read(path);

            return config;
        }
    }
}
