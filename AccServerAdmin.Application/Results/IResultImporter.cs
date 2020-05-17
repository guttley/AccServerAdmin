using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.Results
{
    public interface IResultImporter
    {
        Task Execute(Guid serverId, string serverName);
    }
}