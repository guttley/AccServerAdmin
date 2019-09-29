using System;
using System.Threading.Tasks;


namespace AccServerAdmin.Application.Common
{
    public interface IServerDirectoryResolver
    {
        Task<string> ResolveAsync(Guid serverId);
    }
}
