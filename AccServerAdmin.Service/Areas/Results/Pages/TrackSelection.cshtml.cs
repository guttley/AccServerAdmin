using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class TrackSelectionModel : PageModel
    {
        private readonly ITracksWithSessionsQuery _query;

        public IList<string> Tracks { get; set; }

        [BindProperty]
        public SelectList DaysHistorySelection { get; set; }

        public int DaysHistory { get; set; }

        public TrackSelectionModel(ITracksWithSessionsQuery query)
        {
            _query = query;
        }

        public async Task OnGet(int daysHistory)
        {
            DaysHistory = daysHistory < 5 ? 30 : daysHistory;

            DaysHistorySelection = new SelectList(new [] { 5, 10, 20, 30, 60, 90, 180}, DaysHistory);
            Tracks = await _query.Execute(DaysHistory).ConfigureAwait(false);
        }

    }
}