using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.EntryLists
{
    public class EditEntryModel : PageModel
    {
        private readonly IGetEntryByIdQuery _getEntryByIdQuery;
        private readonly IUpdateEntryCommand _updateEntryCommand;
        private readonly IGetDriverListQuery _getDriverListQuery;
        private readonly IAddDriverEntryCommand _addDriverCommand;
        private readonly IDeleteDriverEntryCommand _deleteDriverCommand;

        public EditEntryModel(
            IGetEntryByIdQuery getEntryByIdQuery,
            IUpdateEntryCommand updateEntryCommand,
            IGetDriverListQuery getDriverListQuery,
            IAddDriverEntryCommand addDriverCommand,
            IDeleteDriverEntryCommand deleteDriverCommand)
        {
            _getEntryByIdQuery = getEntryByIdQuery;
            _updateEntryCommand = updateEntryCommand;
            _getDriverListQuery = getDriverListQuery;
            _addDriverCommand = addDriverCommand;
            _deleteDriverCommand = deleteDriverCommand;
        }

        [BindProperty] public Guid ServerId { get; set; }

        [BindProperty] public Guid EntryListId { get; set; }

        [BindProperty] public Entry Entry { get; set; }

        public List<Driver> Drivers { get; set; }

        public SelectList CarModels { get; set; }

        private async Task BuildBindingListsAsync()
        {
            var model = Entry?.ForcedCarModel ?? CarModel.NotForced;

            CarModels = new SelectList(ListData.Cars, "Key", "Value", model);

            Drivers = (await _getDriverListQuery.Execute()).ToList();
        }

        public async Task OnGetAsync(Guid serverId, Guid entryListId, Guid id)
        {
            ServerId = serverId;
            EntryListId = entryListId;

            Entry = id == Guid.Empty
                ? new Entry {EntryListId = entryListId}
                : await _getEntryByIdQuery.Execute(id);

            await BuildBindingListsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Entry.Ballast > 100)
            {
                ModelState.AddModelError("Entry.Ballast", "Ballast maximum is 100KG");
            }

            if (Entry.Restrictor > 20)
            {
                ModelState.AddModelError("(Entry.Restrictor", "Restrictor maximum is 20%");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Entry.EntryListId = EntryListId;
                    await _updateEntryCommand.Execute(Entry);
                }
                catch (RaceNumberNotUniqueException nex)
                {
                    ModelState.AddModelError("Entry.RaceNumber", nex.Message);
                }
                catch (GridPositionNotUniqueException gex)
                {
                    ModelState.AddModelError("Entry.DefaultGridPosition", gex.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                await BuildBindingListsAsync();
                return Page();
            }

            return Redirect($"Edit?Id={ServerId}");
        }

        public async Task OnGetSelectDriver(Guid serverId, Guid entryListId, Guid entryId, Guid driverId)
        {
            var driverEntry = new DriverEntry {DriverId = driverId, EntryId = entryId};
            await _addDriverCommand.Execute(driverEntry);
            await OnGetAsync(serverId, entryListId, entryId);
        }

        public async Task OnGetRemoveDriver(Guid serverId, Guid entryListId, Guid entryId, Guid driverId)
        {
            var driverEntry = new DriverEntry {DriverId = driverId, EntryId = entryId};
            await _deleteDriverCommand.Execute(driverEntry);
            await OnGetAsync(serverId, entryListId, entryId);
        }
    }
}