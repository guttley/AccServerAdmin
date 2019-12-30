using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            StatusMessage = "Thank you for confirming your email.";
            return Page();
        }
    }
}
