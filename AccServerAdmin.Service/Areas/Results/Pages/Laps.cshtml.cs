using System;
using System.Linq;
using System.Threading.Tasks;
using AccServerAdmin.Application.Results.Queries;
using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccServerAdmin.Service.Areas.Results.Pages
{
    public class LapsModel : PageModel
    {
        private readonly ISessionLapsQuery _sessionLapsQuery;

        [BindProperty]
        public Session Session { get; set; }

        public LapsModel(ISessionLapsQuery sessionLapsQuery)
        {
            _sessionLapsQuery = sessionLapsQuery;
        }

        public async Task OnGetAsync(Guid sessionId, Guid carId)
        {
            Session = await _sessionLapsQuery.Execute(sessionId, carId).ConfigureAwait(false);

            var validLaps = Session.Laps.Where(l => l.Valid).ToList();

            Session.BestLap = validLaps.Min(l => (int) l.LapTime);
            Session.BestSplit1 = validLaps.Min(l => (int)l.Split1);
            Session.BestSplit2 = validLaps.Min(l => (int)l.Split2);
            Session.BestSplit3 = validLaps.Min(l => (int)l.Split3);

            var best = new SessionLap
            {
                Lap = 9999,
                Driver = new Driver {Firstname = "Theoretical", Lastname = "Best"},
                LapTime = Session.BestSplit1 + Session.BestSplit2 + Session.BestSplit3,
                Split1 = Session.BestSplit1,
                Split2 = Session.BestSplit2,
                Split3 = Session.BestSplit3,
            };

            Session.Laps.Add(best);
        }

    }
}