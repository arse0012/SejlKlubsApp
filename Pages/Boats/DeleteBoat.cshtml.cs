using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    { 
        [BindProperty]
        public Models.Boat Boat { get; set; }

        private IBoatService boatService;

        public DeleteBoatModel(IBoatService service)
        {
            boatService = service;
        }

        public async Task OnGetAsync(int id)
        {
            Boat = await boatService.GetBoatByIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await boatService.DeleteBoatAsync(Boat);
            return RedirectToPage("GetAllBoats");
        }
    }
}