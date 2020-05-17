using System;
using System.Threading.Tasks;
using AccServerAdmin.Domain.Results;

namespace AccServerAdmin.Application.Results.Queries
{
    public interface ISessionLapsQuery
    {
        Task<Session> Execute(Guid sessionId, Guid carId);
    }
}
