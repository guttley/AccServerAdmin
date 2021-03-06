﻿using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Drivers.Commands
{
    public interface IUpdateDriverCommand
    {
        Task Execute(Driver driver);
    }
}
