using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Common
{
    public interface IServerPathResolver
    {
        Task<string> Execute(Guid serverId);
    }
}