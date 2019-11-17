using System.Threading.Tasks;

namespace AccServerAdmin.Notifications.EntryList
{
    public interface IEntryImport
    {
        Task ImportMessage(string message);
    }
}
