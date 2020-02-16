using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Drivers
{
    public class EditDriverModel : PageModel
    {
        private readonly IGetDriverByIdQuery _getDriverByIdQuery;
        private readonly ICreateDriverCommand _createDriverCommand;
        private readonly IUpdateDriverCommand _updateDriverCommand;

        public EditDriverModel(
            IGetDriverByIdQuery getDriverByIdQuery,
            ICreateDriverCommand createDriverCommand,
            IUpdateDriverCommand updateDriverCommand)
        {
            _getDriverByIdQuery = getDriverByIdQuery;
            _createDriverCommand = createDriverCommand;
            _updateDriverCommand = updateDriverCommand;
        }

        [BindProperty]
        public Driver Driver { get; set; }

        public SelectList DriverTypes { get; set; }

        private void BuildBindingLists()
        {
            DriverTypes = new SelectList(ListData.DriverTypes, "Key", "Value", Driver.DriverCategory);
        }

        public async Task OnGetAsync(Guid id)
        {
            Driver = id == Guid.Empty
                     ? new Driver() 
                     : await _getDriverByIdQuery.Execute(id).ConfigureAwait(false);

            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Driver.Id == Guid.Empty)
                    {
                        await _createDriverCommand.Execute(Driver).ConfigureAwait(false);
                    } 
                    else 
                    {
                        await _updateDriverCommand.Execute(Driver).ConfigureAwait(false);
                    }
                }
                catch (SteamIdNotUniqueException nex)
                {
                    ModelState.AddModelError("Driver.PlayerId", nex.Message);
                }
                catch (EmptyDirectoryException eex)
                {
                    ModelState.AddModelError(string.Empty, eex.Message);
                }
            }

            if (!ModelState.IsValid)
            {
                BuildBindingLists();
                return Page();
            }
            
            return Redirect("List");
        }

    }
}
