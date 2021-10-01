using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {
        [BindProperty]
        public Models.Booking Booking { get; set; }
        private IBookingService bookingService;
        public DeleteBookingModel(IBookingService service)
        {
            bookingService = service;
        }
        public async Task OnGetAsync(int id)
        {
            Booking = await bookingService.GetBookingByIdAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await bookingService.DeleteBookingAsync(Booking);
            return RedirectToPage("GetAllBookings");
        }
    }
}