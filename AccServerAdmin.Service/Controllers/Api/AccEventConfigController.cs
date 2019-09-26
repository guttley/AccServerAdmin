using System;
using AccServerAdmin.Application.Common;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccServerAdmin.Service.Controllers.Api
{
    [Authorize]
    [Route("api/accEventConfig")]
    [ApiController]
    public class AccEventConfigController : ControllerBase
    {
        private readonly ISaveConfigCommand<EventConfiguration> _saveConfigCommand;
        private readonly IGetConfigByIdQuery<EventConfiguration> _getConfigQuery;

        public AccEventConfigController(
            ISaveConfigCommand<EventConfiguration> saveConfigCommand,
            IGetConfigByIdQuery<EventConfiguration> getConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
            _getConfigQuery = getConfigQuery;
        }

        /// <summary>
        /// GET api/accEventConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public EventConfiguration GetGameConfig([FromQuery] Guid serverId)
        {
            return _getConfigQuery.Execute(serverId);
        }

        /// <summary>
        /// PUT api/accEventConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void SaveGameConfig([FromQuery] Guid serverId, [FromBody] EventConfiguration config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }

    }
}
