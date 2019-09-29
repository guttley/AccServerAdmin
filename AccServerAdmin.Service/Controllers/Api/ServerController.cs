using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Application.Servers.Queries;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccServerAdmin.Service.Controllers.Api
{
    /*
    [Authorize]
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
        public async Task<IEnumerable<Server>> GetServerList()
        {
            return await _getServerListQuery.ExecuteAsync();
        }

        /// <summary>
        /// GET api/server/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public async Task<Server> GetServerById([FromQuery] Guid serverId)
        {
            return await _getServerByIdQuery.ExecuteAsync(serverId);
        }

        /// <summary>
        /// POST api/Server
        /// </summary>
        [HttpPost("{serverName}")]
        public async Task<Server> CreateServer([FromQuery] string serverName)
        {
            return await _createServerCommand.ExecuteAsync(serverName);
        }

        /// <summary>
        /// PUT api/server/{serverId}/{serverName} 
        /// </summary>
        [HttpPut("{serverId}")]
        public async Task UpdateServerAsync([FromQuery] Guid serverId, [FromBody] Server server)
        {
            await _updateServerCommand.ExecuteAsync(server);
        }

        /// <summary>
        /// DELETE api/server/{serverId}
        /// </summary>
        [HttpDelete("{serverId}")]
        public async Task DeleteServer([FromQuery] Guid serverId)
        {
            await _deleteServerCommand.ExecuteAsync(serverId);
        }
    }
    */
}
