using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Bookings
{
    public class GetAllBookingsModel : PageModel
    {
        public IEnumerable<Models.Booking> Bookings { get; private set; }
        private IBookingService bookingService;
        private ISailorService sailorService;
        public Sailor Sailor { get; set; }
        public GetAllBookingsModel(IBookingService service, ISailorService sailorService)
        {
            bookingService = service;
            this.sailorService = sailorService;
        }
        public async Task OnGetAsync(int id)
        {
            Bookings = await bookingService.GetAllBookingsAsync();
        }
        public async Task OnGetMyBookingsAsync(int cid)
        {
            Bookings = await bookingService.GetBookingsBySailorId(cid);
        }
    }
}