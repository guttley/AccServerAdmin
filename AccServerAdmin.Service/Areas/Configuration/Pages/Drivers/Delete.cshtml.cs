using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Commands;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Drivers
{
    public class DeleteDriverModel : PageModel
    {
        private readonly IGetDriverByIdQuery _getDriverByIdQuery;
        private readonly IDeleteDriverCommand _deleteDriverCommand;

        public DeleteDriverModel(
            IGetDriverByIdQuery getDriverByIdQuery,
            IDeleteDriverCommand deleteDriverCommand)
        {
            _getDriverByIdQuery = getDriverByIdQuery;
            _deleteDriverCommand = deleteDriverCommand;
        }

        [BindProperty]
        public Driver Driver { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Driver = await _getDriverByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteDriverCommand.Execute(Driver.Id);
            return RedirectToPage("./List");
        }

    }
}

