using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccServerAdmin.Service.Controllers.Api
{
    /*
    [Authorize]
    [Route("api/accServerConfig")]
    [ApiController]
    public class AccServerConfigController : ControllerBase
    {
        private readonly ISaveConfigCommand<ServerConfiguration> _saveConfigCommand;
        private readonly IGetConfigByIdQuery<ServerConfiguration> _getConfigQuery;

        public AccServerConfigController(
            ISaveConfigCommand<ServerConfiguration> saveConfigCommand,
            IGetConfigByIdQuery<ServerConfiguration> getConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
            _getConfigQuery = getConfigQuery;
        }

        /// <summary>
        /// GET api/accServerConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public ServerConfiguration GetServerConfig([FromQuery] Guid serverId)
        {
            return _getConfigQuery.Execute(serverId);
        }

        /// <summary>
        /// PUT api/accServerConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void SaveServerConfig([FromQuery] Guid serverId, [FromBody] ServerConfiguration config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }
    }
    */
}
