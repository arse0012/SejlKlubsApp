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
    public class DeleteSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        private readonly ISailorService sailorService;
        public string InfoText { get; set; }
        public DeleteSailorModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            InfoText = "Du sletter en sejler! Er du sikker?";
            Sailor = await sailorService.GetSailorByIdAsync(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await sailorService.DeleteSailorAsync(Sailor);
            }
            catch(Exception e)
            {
                InfoText = $"Noget gik galdt! {e.Message}";
                return Page();
            }            
            return RedirectToPage("GetAllSailors");
        }
    }
}