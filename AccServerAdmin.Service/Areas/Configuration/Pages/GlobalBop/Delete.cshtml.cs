using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Bop.Commands;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.GlobalBop
{
    public class DeleteBopModel : PageModel
    {
        private readonly IGetBopByIdQuery _getBopByIdQuery;
        private readonly IDeleteBopCommand _deleteBopCommand;

        public DeleteBopModel(
            IGetBopByIdQuery getBopByIdQuery,
            IDeleteBopCommand deleteBopCommand)
        {
            _getBopByIdQuery = getBopByIdQuery;
            _deleteBopCommand = deleteBopCommand;
        }

        [BindProperty]
        public BalanceOfPerformance Balance { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Balance = await _getBopByIdQuery.ExecuteAsync(id).ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteBopCommand.ExecuteAsync(Balance.Id).ConfigureAwait(false);
            return RedirectToPage("./List");
        }

    }
}

