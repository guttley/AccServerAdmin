using System.Threading.Tasks;

namespace AccServerAdmin.Application.AppSettings
{
    using AccServerAdmin.Domain;

    public interface ISaveAppSettingsCommand
    {
        Task Execute(AppSettings appSettings);
    }
}
