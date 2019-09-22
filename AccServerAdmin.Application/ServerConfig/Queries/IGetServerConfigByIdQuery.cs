using System;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.ServerConfig.Queries
{
    public interface IGetServerConfigByIdQuery
    {
        Configuration Execute(Guid serverId);
    }
}
