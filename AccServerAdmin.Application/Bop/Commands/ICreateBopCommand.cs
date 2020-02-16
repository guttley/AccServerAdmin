using System.Threading.Tasks;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Application.Bop.Commands
{
    public interface ICreateBopCommand
    {
        Task<BalanceOfPerformance> Execute(BalanceOfPerformance bop);
    }
}
