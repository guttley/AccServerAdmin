using AccServerAdmin.Application.Servers.Commands.CreateServer;
using AccServerAdmin.Application.Servers.Queries.GetServerList;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AccServerAdmin.Controllers
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
        public IEnumerable<Server> Get()
        {
            return _getServerListQuery.Execute();
        }

        /// <summary>
        /// GET api/server/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public Server Get(Guid serverId)
        {
            return _getServerByIdQuery.Execute(serverId);
        }

        /// <summary>
        /// POST api/Server
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string serverName)
        {
            _createServerCommand.Execute(serverName);
        }

        /// <summary>
        /// PUT api/server/{serverId}/{serverName} 
        /// </summary>
        [HttpPut("{serverId}")]
        public void Put(Guid serverId, [FromBody] string serverName)
        {
            _updateServerCommand.Execute(serverId, serverName);
        }

        /// <summary>
        /// DELETE api/server/{serverId}
        /// </summary>
        /// <param name="serverId"></param>
        [HttpDelete("{serverId}")]
        public void Delete(Guid serverId)
        {
            _deleteServerCommand.Execute(serverId);
        }
    }
}
