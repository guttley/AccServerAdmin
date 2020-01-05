using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Notifications.EntryList;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AccServerAdmin.Application.Entries.Commands
{
    public class ImportEntryListCommand : IImportEntryListCommand
    {
        private readonly IHubContext<EntryImportHub, IEntryImport> _hubContext;
        private readonly IEntryListReader _entryListReader;
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportEntryListCommand(
            IHubContext<EntryImportHub, IEntryImport> hubContext,
            IEntryListReader entryListReader,
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _entryListReader = entryListReader;
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid serverId)
        {
            var drivers = await _entryListReader.ExecuteAsync(serverId).ConfigureAwait(false);

            foreach (var driver in drivers)
            {
                var existingDriver = await _driverRepository.GetQueryable().AnyAsync(d => d.PlayerId == driver.PlayerId);

                if (existingDriver)
                {
                    await _hubContext.Clients.All.ImportMessage($"Driver already exists: {driver.PlayerId} - {driver.Firstname} {driver.Lastname}").ConfigureAwait(false) ;
                }
                else
                {
                    await _driverRepository.AddAsync(driver).ConfigureAwait(false);
                    await _hubContext.Clients.All.ImportMessage($"Driver imported: {driver.PlayerId} - {driver.Firstname} {driver.Lastname}").ConfigureAwait(false);
                }
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            await _hubContext.Clients.All.ImportMessage("Import Finished").ConfigureAwait(false);
        }
    }
}
