using AccServerAdmin.Domain.AccConfig;
using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Sessions.Queries
{
    public interface IGetSessionByIdQuery
    {
        Task<SessionConfiguration> ExecuteAsync(Guid sesisonId);
    }
}
