using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private IWebHostEnvironment webHostEnvironment;
        public string RndPass { get; private set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public NewSailorModel(ISailorService service, IWebHostEnvironment webHostEnvironment)
        {
            sailorService = service;
            this.webHostEnvironment = webHostEnvironment;
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
                if (Photo != null)
                {
                    if (sailor.SailorImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "photos", sailor.SailorImage);
                        System.IO.File.Delete(filePath);
                    }
                    sailor.SailorImage = ProcessUploadedFile();
                }
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
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "photos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}