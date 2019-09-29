using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Common
{
    public interface IGetConfigByIdQuery<T> where T : new()
    {
        Task<T> ExecuteAsync(Guid serverId);
    }
}
