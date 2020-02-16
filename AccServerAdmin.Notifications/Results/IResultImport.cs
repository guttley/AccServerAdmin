using System.Threading.Tasks;

namespace AccServerAdmin.Notifications.Results
{
    public interface IResultImport
    {
        Task ImportMessage(string message);
    }
}
