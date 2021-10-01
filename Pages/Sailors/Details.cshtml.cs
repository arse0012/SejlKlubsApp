using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class DetailsModel : PageModel
    {
        private ISailorService sailorService;
        [BindProperty]
        public Sailor Sailor { get; set; }
        public DetailsModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Sailor = await sailorService.GetSailorByIdAsync((int)id);

            if (Sailor == null)
                return NotFound();

            return Page();
        }
        public IActionResult OnPostUpdateSailor(int id)
        {
            return RedirectToPage("/Sailors/EditSailor", new { id = id });
        }
        public IActionResult OnPostChangePassword(int id)
        {
            return RedirectToPage("/Sailors/ChangePassword", new { id = id });
        }
        public IActionResult OnPostDeleteSailor(int id)
        {
            return RedirectToPage("/Sailors/DeleteSailor", new { id = id });
        }
    }
}