using System.Collections.Generic;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class TrackLapsModel : PageModel
    {
        private readonly ITrackLapsQuery _trackLapsQuery;

        [BindProperty] public string Track { get; set; }

        [BindProperty]
        public IList<SessionLap> Laps { get; set; } = new List<SessionLap>();

        public TrackLapsModel(ITrackLapsQuery trackLapsQuery)
        {
            _trackLapsQuery = trackLapsQuery;
        }

        public async Task OnGetAsync(string track)
        {
            Track = track;
            Laps = await _trackLapsQuery.Execute(track).ConfigureAwait(false);
        }

    }

}