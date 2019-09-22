using System;

namespace AccServerAdmin.Application.Common
{
    public interface ISaveConfigCommand<T> where T : new()
    {
        void Execute(Guid serverId, T config);
    }
}
