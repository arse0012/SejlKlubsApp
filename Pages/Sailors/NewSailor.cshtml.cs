using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class NewSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        ISailorService sailorService { get; set; }
        public NewSailorModel(ISailorService service)
        {
            sailorService = service;
        }
        public IActionResult OnPost(Sailor sailor)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            sailorService.AddSailorAsync(sailor);
            return RedirectToPage("GetAllSailors");
        }
    }
}