using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Common
{
    public interface IServerConfigWriter
    {
        Task ExecuteAsync(Guid serverId);
    }
}
