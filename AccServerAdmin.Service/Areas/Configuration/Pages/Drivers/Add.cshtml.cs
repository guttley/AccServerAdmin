using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Drivers
{
    public class AddDriverModel : PageModel
    {
        private readonly ICreateDriverCommand _createDriverCommand;

        public AddDriverModel(ICreateDriverCommand createDriverCommand)
        {
            _createDriverCommand = createDriverCommand;
        }

        [BindProperty]
        public Driver Driver { get; set; }

        public SelectList DriverTypes { get; set; }

        private void BuildBindingLists()
        {

            var driverTypes = new Dictionary<DriverCategory, string>
            {
                { DriverCategory.Bronze, "Bronze"},
                { DriverCategory.Silver, "Silver"},
                { DriverCategory.Gold, "Gold"},
                { DriverCategory.Platinum, "Platinum"},
            };

            DriverTypes = new SelectList(driverTypes, "Key", "Value");
        }

        public void OnGet()
        { 
            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Driver = await _createDriverCommand.ExecuteAsync(Driver).ConfigureAwait(false);
                }
                catch (NonUniqueNameException nex)
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
                return Page();
            }
            
            return Redirect($"Edit?Id={Driver.Id}");
        }

    }
}
