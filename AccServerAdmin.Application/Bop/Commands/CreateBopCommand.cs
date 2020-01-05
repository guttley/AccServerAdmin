using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Bop.Commands
{
    public class CreateBopCommand : ICreateBopCommand
    {
        private readonly IBopRepository _bopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBopCommand(
            IBopRepository bopRepository,
            IUnitOfWork unitOfWork)
        {
            _bopRepository = bopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BalanceOfPerformance> ExecuteAsync(BalanceOfPerformance bop)
        {
            if (!await _bopRepository.IsUniqueBopAsync(bop).ConfigureAwait(false))
            {
                throw new BopNotUniqueException("Balance of performance already exists for this track/car");
            }

            await _bopRepository.AddAsync(bop);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return bop;
        }
    }
}