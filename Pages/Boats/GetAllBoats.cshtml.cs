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
    public class GetAllBoatsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public IEnumerable<Models.Boat> Boats { get; private set; }
        private IBoatService boatService;

        public GetAllBoatsModel(IBoatService service)
        {
            boatService = service;
        }

        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Boats = await boatService.GetBoatByNameAsync(FilterCriteria);
            }
            else
                Boats = await boatService.GetAllBoatsAsync();
        }

    }   
}