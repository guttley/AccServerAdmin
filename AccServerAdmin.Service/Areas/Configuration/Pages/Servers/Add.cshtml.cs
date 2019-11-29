using System.Threading.Tasks;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Application.Servers.Commands;
using AccServerAdmin.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
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
                try
                {
                    Server = await _createServerCommand.ExecuteAsync(Server.Name).ConfigureAwait(false);
                }
                catch (DriverNameNotUniqueException nex)
                {
                    ModelState.AddModelError("Server.Name", nex.Message);
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
            else
            {
                return Redirect($"Edit?Id={Server.Id}");
            }
        }

    }
}
