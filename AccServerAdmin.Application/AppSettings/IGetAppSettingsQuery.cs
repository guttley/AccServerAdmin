using System;
using System.Threading.Tasks;

namespace AccServerAdmin.Application.AppSettings
{
    using AccServerAdmin.Domain;

    public interface IGetAppSettingsQuery
    {
        Task<AppSettings> ExecuteAsync();
    }
}
