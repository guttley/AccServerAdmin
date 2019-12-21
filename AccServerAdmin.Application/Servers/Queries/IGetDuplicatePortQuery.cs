﻿using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Servers.Queries
{
    public interface IGetDuplicatePortQuery
    {
        Task<bool> ExecuteAsync(Guid serverId, int tcpPort, int udpPort);
    }
}
