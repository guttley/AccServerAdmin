using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Common
{
    public interface ISaveConfigCommand<T> where T : new()
    {
        Task ExecuteAsync(Guid serverId, T config);
    }
}
