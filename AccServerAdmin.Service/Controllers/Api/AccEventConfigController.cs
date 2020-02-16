namespace AccServerAdmin.Service.Controllers.Api
{
    /*
    [Authorize]
    [Route("api/accEventConfig")]
    [ApiController]
    public class AccEventConfigController : ControllerBase
    {
        private readonly ISaveConfigCommand<EventCfg> _saveConfigCommand;
        private readonly IGetConfigByIdQuery<EventCfg> _getConfigQuery;

        public AccEventConfigController(
            ISaveConfigCommand<EventCfg> saveConfigCommand,
            IGetConfigByIdQuery<EventCfg> getConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
            _getConfigQuery = getConfigQuery;
        }

        /// <summary>
        /// GET api/accEventConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public EventCfg GetGameConfig([FromQuery] Guid serverId)
        {
            return _getConfigQuery.Execute(serverId);
        }

        /// <summary>
        /// PUT api/accEventConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public void SaveGameConfig([FromQuery] Guid serverId, [FromBody] EventCfg config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }
    }
    */
}
