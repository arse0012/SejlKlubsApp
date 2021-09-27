using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;
using SejlKlubsApp.Services.Service;

namespace SejlKlubsApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public LogInService logService;
        private IBookingService bookingService;
        private ISailorService sailorService;
        public List<Booking> Bookings { get; set; }
        public List<Boat> Boats { get; set; }
        [BindProperty]
        public Sailor Sailor { get; set; }
        public ArrayList DatesArrayList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, LogInService logService, IBookingService bookingService, ISailorService sailorService)
        {
            _logger = logger;
            this.sailorService = sailorService;
            this.logService = logService;
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if(logService.GetLoggedSailor() != null)
                {
                    Sailor = await sailorService.GetSailorByIdAsync(logService.GetLoggedSailor().SailorId);
                    Bookings = await bookingService.GetBookingsBySailorId(Sailor.SailorId);
                    Bookings = Bookings.FindAll(e => e.DateTo > DateTime.Now);
                    Bookings.Sort((e1, e2) => e1.DateTo.CompareTo(e2.DateTo));

                    DatesArrayList = Dates().Result;

                    if (Sailor == null)
                        return NotFound();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return Page();
        }
        public async Task<ArrayList> Dates()
        {
            ArrayList list = new ArrayList();
            foreach(var d in Bookings)
            {
                list.Add(d.DateTo);
            }
            return list;
        }
    }
}
