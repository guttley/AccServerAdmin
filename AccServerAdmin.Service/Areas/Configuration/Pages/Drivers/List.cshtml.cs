using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Drivers.Queries;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Drivers
{
    public class DriverListModel : PageModel
    {
        private readonly IGetDriverListQuery _getDriverListQuery;

        public DriverListModel(IGetDriverListQuery getDriverListQuery)
        {
            _getDriverListQuery = getDriverListQuery;
        }

        [BindProperty]
        public IEnumerable<Driver> Drivers { get; set; }

        public async Task OnGetAsync()
        {
            Drivers = await _getDriverListQuery.Execute().ConfigureAwait(false);
        }
    }
}
