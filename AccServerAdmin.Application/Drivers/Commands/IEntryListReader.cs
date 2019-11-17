using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public interface IEntryListReader
    {
        Task<List<Driver>> ExecuteAsync(Guid serverId);
    }
}
