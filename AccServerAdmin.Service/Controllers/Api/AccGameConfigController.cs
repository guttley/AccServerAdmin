namespace AccServerAdmin.Service.Controllers.Api
{
    /*
    [Authorize]
    [Route("api/accGameConfig")]
    [ApiController]
    public class AccGameConfigController : ControllerBase
    {
        private readonly ISaveConfigCommand<GameCfg> _saveConfigCommand;
        private readonly IGetConfigByIdQuery<GameCfg> _getConfigQuery;

        public AccGameConfigController(
            ISaveConfigCommand<GameCfg> saveConfigCommand,
            IGetConfigByIdQuery<GameCfg> getConfigQuery)
        {
            _saveConfigCommand = saveConfigCommand;
            _getConfigQuery = getConfigQuery;
        }

        /// <summary>
        /// GET api/accGameConfig/{serverId}
        /// </summary>
        [HttpGet("{serverId}")]
        public GameCfg GetGameConfig([FromQuery] Guid serverId)
        {
            return await _getConfigQuery.Execute(serverId).ConfigureAwait(false);
        }

        /// <summary>
        /// PUT api/accGameConfig/{serverId}
        /// </summary>
        [HttpPut("{serverId}")]
        public async Task SaveGameConfig([FromQuery] Guid serverId, [FromBody] GameCfg config)
        {
            _saveConfigCommand.Execute(serverId, config);
        }

    }
    */
}
