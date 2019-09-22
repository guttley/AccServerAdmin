using System;
using AccServerAdmin.Application.ServerConfig.Commands;
using AccServerAdmin.Application.ServerConfig.Queries;
using Microsoft.AspNetCore.Mvc;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Service.Controllers
{
    [Route("api/accServerConfig")]
    [ApiController]
    public class AccServerConfigController : ControllerBase
    {
        private readonly ICreateServerConfigCommand _createConfigCommand;
        private readonly IUpdateServerConfigCommand _updateConfigCommand;
        private readonly IGetServerConfigByIdQuery _getServerConfigQuery;

        public AccServerConfigController(
            ICreateServerConfigCommand createConfigCommand,
            IUpdateServerConfigCommand updateConfigCommand,
            IGetServerConfigByIdQuery getServerConfigQuery)
        {
            _createConfigCommand = createConfigCommand;
            _updateConfigCommand = updateConfigCommand;
            _getServerConfigQuery = getServerConfigQuery;
        }

        /// <summary>
        /// GET api/accServerConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public Configuration GetServerConfig(Guid serverId)
        {
            return _getServerConfigQuery.Execute(serverId);
        }

        /// <summary>
        /// POST api/accServerConfig/{serverId}
        /// </summary>
        [HttpPost("{serverId}")]
        public void CreateServerConfig(Guid serverId, [FromBody] Configuration config)
        {
            _createConfigCommand.Execute(serverId, config);
        }

        /// <summary>
        /// PUT api/accServerConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void UpdateServeConfig(Guid serverId, [FromBody] Configuration config)
        {
            _updateConfigCommand.Execute(serverId, config);
        }

    }
}
