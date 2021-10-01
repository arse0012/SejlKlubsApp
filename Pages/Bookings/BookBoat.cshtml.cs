using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.ADO_Service;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Bookings
{
    public class BookBoatModel : PageModel
    {
        [BindProperty]
        public Booking Booking { get; set; }
        private IBookingService bookingService;
        IWebHostEnvironment webHostEnvironment;
        public SelectList SailorCodes { get; set; }
        public BookBoatModel(IBookingService service, ADO_SailorService cService, IWebHostEnvironment webHostEnvironment)
        {
            bookingService = service;
            this.webHostEnvironment = webHostEnvironment;
            Booking = new Booking();
            Task<List<Sailor>> sailors = cService.GetAllSailorsAsync();
            SailorCodes = new SelectList(sailors.Result, "SailorId", "Name");

        }
        public IActionResult OnGet(int id)
        {
            Booking.BoatId = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await bookingService.BookBoatAsync(Booking);
            return RedirectToPage("GetAllBookings");
        }
    }
}