using System;


namespace AccServerAdmin.Application.Common
{
    public interface IServerDirectoryResolver
    {
        string Resolve(Guid serverId);
    }
}
