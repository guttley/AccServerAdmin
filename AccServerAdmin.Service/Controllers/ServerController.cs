using System;
using System.Collections.Generic;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AccServerAdmin.Service.Controllers
{
    [Route("api/server")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ICreateServerCommand _createServerCommand;
        private readonly IDeleteServerCommand _deleteServerCommand;
        private readonly IUpdateServerCommand _updateServerCommand;
        private readonly IGetServerByIdQuery _getServerByIdQuery;
        private readonly IGetServerListQuery _getServerListQuery;

        public ServerController(
            ICreateServerCommand createServerCommand,
            IDeleteServerCommand deleteServerCommand,
            IUpdateServerCommand updateServerCommand,
            IGetServerByIdQuery getServerByIdQuery,
            IGetServerListQuery getServerListQuery)
        {
            _createServerCommand = createServerCommand;
            _deleteServerCommand = deleteServerCommand;
            _updateServerCommand = updateServerCommand;
            _getServerByIdQuery = getServerByIdQuery;
            _getServerListQuery = getServerListQuery;
        }

        /// <summary>
        /// GET api/server
        /// </summary>
        [HttpGet]
        public IEnumerable<Server> GetServerList()
        {
            return _getServerListQuery.Execute();
        }

        /// <summary>
        /// GET api/server/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public Server GetServerById(Guid serverId)
        {
            return _getServerByIdQuery.Execute(serverId);
        }

        /// <summary>
        /// POST api/Server
        /// </summary>
        [HttpPost("{serverName}")]
        public Server CreateServer(string serverName)
        {
            return _createServerCommand.Execute(serverName);
        }

        /// <summary>
        /// PUT api/server/{serverId}/{serverName} 
        /// </summary>
        [HttpPut("{serverId}/{serverName}")]
        public void UpdateServer(Guid serverId, string serverName)
        {
            _updateServerCommand.Execute(serverId, serverName);
        }

        /// <summary>
        /// DELETE api/server/{serverId}
        /// </summary>
        [HttpDelete("{serverId}")]
        public void DeleteServer(Guid serverId)
        {
            _deleteServerCommand.Execute(serverId);
        }
    }
}
