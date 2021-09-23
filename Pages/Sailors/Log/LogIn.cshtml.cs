using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Interfaces;
using SejlKlubsApp.Services.Service;

namespace SejlKlubsApp.Pages.Sailors.Log
{
    public class LogInModel : PageModel
    {
        private ISailorService sailorService;
        private LogInService loginService;
        public string AccessDenied = "";
        [BindProperty]
        public Sailor Sailor { get; set; }
        public LogInModel(ISailorService service, LogInService logIn)
        {
            sailorService = service;
            loginService = logIn;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            foreach (Sailor sailor in sailorService.GetAllSailorsAsync().Result)
            {
                if (sailor.Email == Sailor.Email)
                {
                    if (sailor.Password == Sailor.PasswordCheck)
                    {
                        loginService.SailorLogIn(sailor);
                        return RedirectToPage("/Index");
                    }
                }
                AccessDenied = "Navnet/kodeordet eksistere ikke";
            }
            return Page();
        }
    }
}