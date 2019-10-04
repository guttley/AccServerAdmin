using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.AppSettings
{
    using AccServerAdmin.Domain;   

    public class SaveAppSettingsCommand : ISaveAppSettingsCommand
    {
        private readonly IDataRepository<AppSettings> _appSettingsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveAppSettingsCommand(
            IDataRepository<AppSettings> appSettingsRepository,
            IUnitOfWork unitOfWork)
        {
            _appSettingsRepository = appSettingsRepository;
            _unitOfWork = unitOfWork;
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
                _appSettingsRepository.Update(dbSettings.Id, appSettings);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
