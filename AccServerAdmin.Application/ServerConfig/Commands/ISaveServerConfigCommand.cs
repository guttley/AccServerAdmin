using System;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.ServerConfig.Commands
{
    public interface ISaveServerConfigCommand
    {
        void Execute(Guid serverId, Configuration config);
    }
}
