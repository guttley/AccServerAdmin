using System;
using System.Collections.Generic;
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
            var driverTypes = new Dictionary<int, string>
            {
                { (int)DriverCategory.Bronze, "Bronze"},
                { (int)DriverCategory.Silver, "Silver"},
                { (int)DriverCategory.Gold, "Gold"},
                { (int)DriverCategory.Platinum, "Platinum"},
            };

            DriverTypes = new SelectList(driverTypes, "Key", "Value", Driver.DriverCategory);
        }

        public async Task OnGetAsync(Guid id)
        {
            Driver = id == Guid.Empty
                     ? new Driver() 
                     : await _getDriverByIdQuery.ExecuteAsync(id).ConfigureAwait(false);

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
                        await _createDriverCommand.ExecuteAsync(Driver).ConfigureAwait(false);
                    } 
                    else 
                    {
                        await _updateDriverCommand.ExecuteAsync(Driver).ConfigureAwait(false);
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
