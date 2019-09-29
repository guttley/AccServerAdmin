using System.Threading.Tasks;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages
{
    public class AddServerModel : PageModel
    {
        private readonly ICreateServerCommand _createServerCommand;

        public AddServerModel(ICreateServerCommand createServerCommand)
        {
            _createServerCommand = createServerCommand;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

            }


            return Page();
        }

    }
}
