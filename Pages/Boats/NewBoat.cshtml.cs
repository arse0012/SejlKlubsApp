using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SejlKlubsApp.Models;
using SejlKlubsApp.Interfaces;

namespace SejlKlubsApp.Pages.Boats
{
    public class NewBoatModel : PageModel
    {
        [BindProperty]
        public Models.Boat Boat { get; set; }

        private IBoatService boatService { get; set; }
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public IFormFile Photo { get; set; }

        public NewBoatModel(IBoatService service, IWebHostEnvironment webHostEnvironment)
        {
            boatService = service;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Models.Boat boat)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Photo != null)
            {
                if(boat.ImageName != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", boat.ImageName);
                    System.IO.File.Delete(filePath);
                }
                boat.ImageName = ProcessUploadedFile();
            }
            await boatService.AddBoatAsync(boat);
            return RedirectToPage("GetAllBoats");
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
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