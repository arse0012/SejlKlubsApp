using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Services.Service;
using SejlKlubsApp.Services.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class ChangePasswordModel : PageModel
    {
        private ISailorService sailorService;
        public LogInService logService;
        private string _newPassword;
        public string AccessDenied = "";
        public ChangePasswordModel(ISailorService sailorService, LogInService logService)
        {
            this.sailorService = sailorService;
            this.logService = logService;
        }
        [BindProperty]
        public Sailor Sailor { get; set; }
        public IActionResult OnPost(int? id)
        {
            if (sailorService.GetSailorByIdAsync((int)id).Result.Password == Sailor.PasswordCheck)
            {
                _newPassword = Sailor.Password;
                Sailor = sailorService.GetSailorByIdAsync((int)id).Result;
                Sailor.Password = _newPassword;

                sailorService.UpdateSailorAsync(Sailor);
                return RedirectToPage("/Sailors/Details", new { id = id });
            }

            AccessDenied = "Wrong Password";
            return Page();
        }
        
    }
}