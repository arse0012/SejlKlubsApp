using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SejlKlubsApp.Pages.Admin
{
    public class AdminHubModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPostCreateBoat()
        {
            return RedirectToPage("/Boats/NewBoat");
        }
        public IActionResult OnPostCreateSailor()
        {
            return RedirectToPage("/Sailors/NewSailor");
        }
        public IActionResult OnPostAllBoats()
        {
            return RedirectToPage("/Boats/GetAllBoats");
        }
        public IActionResult OnPostAllSailors()
        {
            return RedirectToPage("/Sailors/GetAllSailors");
        }
    }
}