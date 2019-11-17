using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AccServerAdmin.Notifications.EntryList
{
    public class EntryImportHub : Hub<IEntryImport>
    {
        public async Task ImportMessageAsync(string message)
        {
            await Clients.All.ImportMessage(message);
        }
    }
}
