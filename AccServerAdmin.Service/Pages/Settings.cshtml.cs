using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AccServerAdmin.Service.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IAppSettingsRepository _appSettingsRepository;

        public class DirectoryModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string ServerBase { get; set; }

            [Required]
            [DataType(DataType.Text)]
            public string InstanceBase { get; set; }
        }

        [BindProperty]
        public DirectoryModel Settings { get; set; }

        public SettingsModel(IAppSettingsRepository appSettingsRepository)
        {
            _appSettingsRepository = appSettingsRepository;
        }

        public void OnGet()
        {
            _appSettingsRepository.Read();

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            /*
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            */
            // If we got this far, something failed, redisplay form
            return Page();
            
        }
    }
}