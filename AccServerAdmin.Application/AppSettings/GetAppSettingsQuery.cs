using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;

namespace AccServerAdmin.Application.AppSettings
{
    using AccServerAdmin.Domain;   

    public class GetAppSettingsQuery : IGetAppSettingsQuery
    {
        private readonly IDataRepository<AppSettings> _appSettingsRepository;

        public GetAppSettingsQuery(IDataRepository<AppSettings> appSettingsRepository)
        {
            _appSettingsRepository = appSettingsRepository;
        }

        public async Task<AppSettings> Execute()
        {
            var settings = await _appSettingsRepository.GetAll();

            return settings.FirstOrDefault();
        }
    }
}
