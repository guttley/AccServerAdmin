using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AccServerAdmin.Notifications.Results
{
    public class ResultImportHub : Hub<IResultImport>
    {
        public async Task ImportMessageAsync(string message)
        {
            await Clients.All.ImportMessage(message);
        }
    }
}
