using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Interfaces;
using SejlKlubsApp.Services.Service;

namespace SejlKlubsApp.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatService boatService;
        public LogInService logInService;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public IEnumerable<Boat> Boats { get; private set; }
        
        private IBookingService bookingService;
        public List<Booking> userBookings;
        public Sailor currentSailor { get; set; }

        public IndexModel(IBoatService service, IBookingService bookingService, LogInService logInService)
        {
            boatService = service;
            this.logInService = logInService;
            this.bookingService = bookingService;
        }
        public async Task OnGetAsync()
        {
            
            //currentSailor = logInService.GetLoggedSailor();
            //if (currentSailor != null)
            //{
            //    userBookings = bookingService.GetBookingsBySailorId(currentSailor.SailorId).Result;
            //}
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Boats = await boatService.GetBoatByNameAsync(FilterCriteria);
            }
            else
                Boats = await boatService.GetAllBoatsAsync();
        }
    }
}