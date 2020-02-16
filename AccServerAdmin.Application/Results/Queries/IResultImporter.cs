using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Results.Queries
{
    public interface IResultImporter
    {
        Task Execute(Guid serverId, string serverName);
    }
}