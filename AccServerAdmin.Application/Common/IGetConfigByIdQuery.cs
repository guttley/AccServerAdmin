using System;

namespace AccServerAdmin.Application.Common
{
    public interface IGetConfigByIdQuery<T> where T : new()
    {
        T Execute(Guid serverId);
    }
}
