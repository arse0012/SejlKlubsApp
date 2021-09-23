using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Exceptions;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class NewSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        public string InfoText { get; set; }
        public IEnumerable<Sailor> Sailors { get; private set; }
        private readonly ISailorService sailorService;
        public string RndPass { get; private set; }
        public NewSailorModel(ISailorService service)
        {
            sailorService = service;
        }
        public async Task OnGetAsync()
        {
            InfoText = "Registre ny sejler";
            RndPass = CreatePassword(5);
            Sailors = await sailorService.GetAllSailorsAsync();
        }
        public string CreatePassword(int length)
        {
            const string vaild = "abcdefghijkmnopqrstuvwxzyABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder bld = new StringBuilder();
            Random rnd = new Random();
            while(0 < length--)
            {
                bld.Append(vaild[rnd.Next(vaild.Length)]);
            }
            return bld.ToString();
        }
        public async Task<IActionResult> OnPostAsync(Sailor sailor)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await sailorService.AddSailorAsync(sailor);
                Sailors = await sailorService.GetAllSailorsAsync();
                
            }
            catch(ExistsException e)
            {
                InfoText = $"Something went wrong! {e.Message}";
                RndPass = CreatePassword(5);
                return Page();
            }
            catch(Exception e)
            {
                InfoText = $"Something went wrong! {e.Message}";
                RndPass = CreatePassword(5);
                return Page();
            }
            return RedirectToPage("GetAllSailors");
        }
    }
}