﻿using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Common
{
    public interface IServerInstanceCleanUp
    {
        Task Execute(Guid serverId);
    }
}
