using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.GlobalBop
{
    public class BopListModel : PageModel
    {
        private readonly IGetBopListQuery _getBopListQuery;

        public BopListModel(IGetBopListQuery getBopListQuery)
        {
            _getBopListQuery = getBopListQuery;
        }

        [BindProperty]
        public IEnumerable<BalanceOfPerformance> BalanceList { get; set; }

        public async Task OnGetAsync()
        {
            BalanceList = await _getBopListQuery.ExecuteAsync().ConfigureAwait(false);
        }
    }
}
