using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AccServerAdmin.Service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(
            ILogger<IndexModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    
    }
}
