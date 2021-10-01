using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Exceptions;
using SejlKlubsApp.Models;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Sailors
{
    public class EditSailorModel : PageModel
    {
        [BindProperty]
        public Sailor Sailor { get; set; }
        ISailorService sailorService { get; set; }
        public IEnumerable<Sailor> Sailors { get; private set; }
        public string InfoText { get; set; }
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public IFormFile Photo { get; set; }
        public EditSailorModel(ISailorService service, IWebHostEnvironment webHost)
        {
            sailorService = service;
            webHostEnvironment = webHost;
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
                if (Photo != null)
                {
                    if (sailor.SailorImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "photos", sailor.SailorImage);
                        System.IO.File.Delete(filePath);
                    }
                    sailor.SailorImage = ProcessUploadedFile();
                }
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