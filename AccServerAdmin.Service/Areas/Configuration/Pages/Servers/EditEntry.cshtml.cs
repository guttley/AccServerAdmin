using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Application.Entries.Commands;
using AccServerAdmin.Application.Entries.Queries;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditEntryModel : PageModel
    {
        private readonly IGetEntryByIdQuery _getEntryByIdQuery;
        private readonly ICreateEntryCommand _createEntryCommand;
        private readonly IUpdateEntryCommand _updateEntryCommand;
        private readonly IGetDriverListQuery _getDriverListQuery;

        public EditEntryModel(
            IGetEntryByIdQuery getEntryByIdQuery,
            ICreateEntryCommand createEntryCommand,
            IUpdateEntryCommand updateEntryCommand,
            IGetDriverListQuery getDriverListQuery)
        {
            _getEntryByIdQuery = getEntryByIdQuery;
            _createEntryCommand = createEntryCommand;
            _updateEntryCommand = updateEntryCommand;
            _getDriverListQuery = getDriverListQuery;
        }

        [BindProperty]
        public Guid ServerId { get; set; }

        [BindProperty]
        public Guid EntryListIdId { get; set; }

        [BindProperty]
        public Entry Entry { get; set; }

        public List<Driver> Drivers { get; set; }

        public SelectList CarModels { get; set; }

        private async Task BuildBindingListsAsync()
        {
            var cars = EnumHelper.GetValues<CarModel>().ToDictionary(model => model, model => model.GetDescription()).OrderBy(p => p.Value);
            var model = Entry?.ForcedCarModel ?? CarModel.NotForced; 

            CarModels = new SelectList(cars, "Key", "Value", model);

            Drivers = (await _getDriverListQuery.ExecuteAsync().ConfigureAwait(false)).ToList();
        }

        public async Task OnGetAsync(Guid serverId, Guid entryListId, Guid id)
        {
            ServerId = serverId;
            EntryListIdId = entryListId;
            
            Entry = id == Guid.Empty
                    ? new Entry { EntryListId = entryListId }
                    : await _getEntryByIdQuery.ExecuteAsync(id).ConfigureAwait(false);

            await BuildBindingListsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Entry.Id == Guid.Empty)
                    {
                        await _createEntryCommand.ExecuteAsync(Entry).ConfigureAwait(false);                
                    }
                    else
                    {
                        await _updateEntryCommand.ExecuteAsync(Entry).ConfigureAwait(false);
                    }

                    return RedirectToPage("./Edit", new { Id = ServerId });
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

    }
}
