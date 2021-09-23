using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Exceptions;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class EditSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        ISailorService sailorService { get; set; }
        public IEnumerable<Sailor> Sailors { get; private set; }
        public string InfoText { get; set; }
        public EditSailorModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task OnGetAsync(int id)
        {
            InfoText = $"Indsæt ændringer her";
            Sailor = await sailorService.GetSailorByIdAsync(id);
            Sailors = await sailorService.GetAllSailorsAsync();
        }
        public async Task <IActionResult> OnPostAsync(Sailor sailor)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await sailorService.UpdateSailorAsync(sailor);
                Sailors = await sailorService.GetAllSailorsAsync();
            }
            catch(ExistsException e)
            {
                InfoText = $"Noget gik galdt! {e.Message}";
                return Page();
            }           
            return RedirectToPage("GetAllSailors");
        }
    }
}