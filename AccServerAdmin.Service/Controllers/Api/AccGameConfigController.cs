using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccServerAdmin.Service.Controllers.Api
{
    /*
    [Authorize]
    [Route("api/accGameConfig")]
    [ApiController]
    public class AccGameConfigController : ControllerBase
    {
        private readonly ISaveConfigCommand<GameConfiguration> _saveConfigCommand;
        private readonly IGetConfigByIdQuery<GameConfiguration> _getConfigQuery;

        public AccGameConfigController(
            ISaveConfigCommand<GameConfiguration> saveConfigCommand,
            IGetConfigByIdQuery<GameConfiguration> getConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
            _getConfigQuery = getConfigQuery;
        }

        /// <summary>
        /// GET api/accGameConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public GameConfiguration GetGameConfig([FromQuery] Guid serverId)
        {
            return await _getConfigQuery.ExecuteAsync(serverId);
        }

        /// <summary>
        /// PUT api/accGameConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public async Task SaveGameConfig([FromQuery] Guid serverId, [FromBody] GameConfiguration config)
        {
            _saveConfigCommand.ExecuteAsync(serverId, config);
        }

    }
    */
}
