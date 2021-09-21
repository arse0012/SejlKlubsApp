using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        [BindProperty]
        public Models.Boat Boat { get; set; }

        private IBoatService boatService { get; set; }

        public EditBoatModel(IBoatService service)
        {
            boatService = service;
        }

        public async Task OnGetAsync(int id)
        {
            Boat = await boatService.GetBoatByIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await boatService.UpdateBoatAsync(Boat);
            return RedirectToPage("GetAllBoats");
        }
    }
}