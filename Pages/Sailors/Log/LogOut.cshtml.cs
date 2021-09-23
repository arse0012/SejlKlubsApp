using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Services.Service;

namespace SejlKlubsApp.Pages.Sailors.Log
{
    public class LogOutModel : PageModel
    {
        private LogInService logInService;
        public LogOutModel(LogInService logIn)
        {
            logInService = logIn;
        }
        public IActionResult OnGet()
        {
            logInService.SailorLogOut();
            return RedirectToPage("");
        }
    }
}