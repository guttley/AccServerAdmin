﻿using System.Threading.Tasks;
using AccServerAdmin.Domain;

namespace AccServerAdmin.Application.Servers.Commands
{
    public interface IUpdateServerCommand
    {
        Task ExecuteAsync(Server server);
    }
}
