using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Common
{
    public interface ISaveConfigCommand<T> where T : class, IKeyedEntity
    {
        Task ExecuteAsync(Guid serverId, T config);
    }
}
