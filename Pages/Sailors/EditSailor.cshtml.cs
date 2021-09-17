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
    public class EditSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        ISailorService sailorService { get; set; }
        public EditSailorModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task OnGetAsync(int id)
        {
            Sailor = await sailorService.GetSailorByIdAsync(id);
        }
        public async Task <IActionResult> OnPostAsync(Sailor sailor)
        {
            await sailorService.UpdateSailorAsync(sailor);
            return RedirectToPage("GetAllSailors");
        }
    }
}