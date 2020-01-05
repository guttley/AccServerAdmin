using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Bop.Commands
{
    public interface IUpdateBopCommand
    {
        Task ExecuteAsync(BalanceOfPerformance bop);
    }
}
