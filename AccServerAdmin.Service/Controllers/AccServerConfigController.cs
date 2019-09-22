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
        private readonly ISaveServerConfigCommand _saveConfigCommand;
        private readonly IGetServerConfigByIdQuery _getServerConfigQuery;

        public AccServerConfigController(
            ISaveServerConfigCommand saveConfigCommand,
            IGetServerConfigByIdQuery getServerConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
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
        /// PUT api/accServerConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void SaveServeConfig(Guid serverId, [FromBody] Configuration config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }

    }
}
