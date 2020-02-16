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

        public async Task<BalanceOfPerformance> Execute(BalanceOfPerformance bop)
        {
            if (!await _bopRepository.IsUniqueBopAsync(bop))
            {
                throw new BopNotUniqueException("Balance of performance already exists for this track/car");
            }

            await _bopRepository.Add(bop);
            await _unitOfWork.SaveChanges();

            return bop;
        }
    }
}