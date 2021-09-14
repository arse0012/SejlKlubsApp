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
    public class NewBoatModel : PageModel
    {
        [BindProperty]
        public Models.Boat Boat { get; set; }

        private IBoatService boatService { get; set; }

        public NewBoatModel(IBoatService service)
        {
            boatService = service;
        }

        public IActionResult OnPost(Models.Boat boat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            boatService.AddBoatAsync(boat);
            return RedirectToPage("GetAllBoats");
        }
    }
}