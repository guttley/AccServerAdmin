using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Bop.Commands
{
    public class UpdateBopCommand : IUpdateBopCommand
    {
        private readonly IBopRepository _bopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBopCommand(
            IBopRepository bopRepository,
            IUnitOfWork unitOfWork)
        {
            _bopRepository = bopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(BalanceOfPerformance bop)
        {
            if (!await _bopRepository.IsUniqueBopAsync(bop).ConfigureAwait(false))
            {
                throw new BopNotUniqueException("Balance of performance already exists for this track/car");
            }

            _bopRepository.Update(bop.Id, bop);
            await _unitOfWork.SaveChanges().ConfigureAwait(false);
        }
    }
}
