using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Bop.Commands;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
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
        public Guid ServerId { get; set; }

        [BindProperty]
        public BalanceOfPerformance Balance { get; set; }

        public async Task OnGetAsync(Guid id, Guid serverId)
        {
            ServerId = serverId;
            Balance = await _getBopByIdQuery.Execute(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _deleteBopCommand.Execute(Balance.Id);
            return Redirect($"/Configuration/Servers/Edit?Id={ServerId}#nav-tab-bop");
        }

    }
}

