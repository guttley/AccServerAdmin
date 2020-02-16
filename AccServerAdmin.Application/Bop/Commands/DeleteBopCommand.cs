using System;
using System.Threading.Tasks;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;

namespace AccServerAdmin.Application.Bop.Commands
{
    public class DeleteBopCommand : IDeleteBopCommand
    {
        private readonly IBopRepository _bopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBopCommand(
            IBopRepository bopRepository,
            IUnitOfWork unitOfWork)
        {
            _bopRepository = bopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid bopId)
        {
            _bopRepository.Delete(bopId);
            await _unitOfWork.SaveChanges();
        }
    }
}
