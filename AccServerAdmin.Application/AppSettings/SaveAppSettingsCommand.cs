using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.AppSettings
{
    using AccServerAdmin.Domain;   

    public class SaveAppSettingsCommand : ISaveAppSettingsCommand
    {
        private readonly IDataRepository<AppSettings> _appSettingsRepository;

        public SaveAppSettingsCommand(IDataRepository<AppSettings> appSettingsRepository)
        {
            _appSettingsRepository = appSettingsRepository;
        }

        public async Task ExecuteAsync(AppSettings appSettings)
        {
            var dbSettings = (await _appSettingsRepository.GetAllAsync().ConfigureAwait(false)).FirstOrDefault();

            if (dbSettings is null)
            {
                await _appSettingsRepository.AddAsync(appSettings).ConfigureAwait(false);
            }
            else
            {
                await _appSettingsRepository.UpdateAsync(dbSettings, appSettings).ConfigureAwait(false);
            }
        }
    }
}
