using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Entries.Queries
{
    public interface IEntryListReader
    {
        Task<List<Driver>> Execute(Guid serverId);
    }
}
