using System;
using AccServerAdmin.Application.Common;
using Microsoft.AspNetCore.Mvc;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Authorization;

namespace AccServerAdmin.Service.Controllers
{
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
        public GameConfiguration GetGameConfig(Guid serverId)
        {
            return _getConfigQuery.Execute(serverId);
        }

        /// <summary>
        /// PUT api/accGameConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void SaveGameConfig(Guid serverId, [FromBody] GameConfiguration config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }

    }
}
