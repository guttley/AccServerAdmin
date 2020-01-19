using System;
using System.Threading.Tasks;
using AccServerAdmin.Application.Bop.Commands;
using AccServerAdmin.Application.Bop.Queries;
using AccServerAdmin.Application.Exceptions;
using AccServerAdmin.Domain.AccConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AccServerAdmin.Service.Areas.Configuration.Pages.Servers
{
    public class EditBopModel : PageModel
    {
        private readonly IGetBopByIdQuery _getBopByIdQuery;
        private readonly ICreateBopCommand _createBopCommand;
        private readonly IUpdateBopCommand _updateBopCommand;

        public EditBopModel(
            IGetBopByIdQuery getBopByIdQuery,
            ICreateBopCommand createBopCommand,
            IUpdateBopCommand updateBopCommand)
        {
            _getBopByIdQuery = getBopByIdQuery;
            _createBopCommand = createBopCommand;
            _updateBopCommand = updateBopCommand;
        }

        [BindProperty] 
        public Guid ServerId { get; set; }

        [BindProperty]
        public BalanceOfPerformance Balance { get; set; }

        public SelectList Tracks { get; set; }

        public SelectList Cars { get; set; }

        private void BuildBindingLists()
        {
            Tracks = new SelectList(ListData.Tracks, "Key", "Value", Balance.Track);
            Cars = new SelectList(ListData.Cars, "Key", "Value", Balance.Car);
        }

        public async Task OnGetAsync(Guid serverId, Guid id)
        {
            ServerId = serverId;

            Balance = id == Guid.Empty
                     ? new BalanceOfPerformance() 
                     : await _getBopByIdQuery.ExecuteAsync(id).ConfigureAwait(false);

            BuildBindingLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Balance.Track))
            {
                ModelState.AddModelError("Balance.Track", "You must select a track");
            }

            if (Balance.Car == CarModel.NotForced)
            {
                ModelState.AddModelError("Balance.Car", "You must select a car");
            }

            if (Balance.Ballast > 100)
            {
                ModelState.AddModelError("Balance.Ballast", "Ballast maximum is 100KG");
            }

            if (Balance.Restrictor > 20)
            {
                ModelState.AddModelError("Balance.Restrictor", "Restrictor maximum is 20%");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Balance.ServerId = ServerId;

                    if (Balance.Id == Guid.Empty)
                    {
                        await _createBopCommand.ExecuteAsync(Balance).ConfigureAwait(false);
                    } 
                    else 
                    {
                        await _updateBopCommand.ExecuteAsync(Balance).ConfigureAwait(false);
                    }
                }
                catch (BopNotUniqueException nex)
                {
                    ModelState.AddModelError("Balance.Track", nex.Message);
                    ModelState.AddModelError("Balance.Car", nex.Message);
                }

                return Redirect($"Edit?Id={ServerId}");
            } 
            else
            { 
                BuildBindingLists();
                return Page();
            }
        }

    }
}
